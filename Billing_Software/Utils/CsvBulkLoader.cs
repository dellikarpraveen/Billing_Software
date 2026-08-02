#nullable enable
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billing_Software.Utils
{
    public partial class CsvBulkLoader : System.ComponentModel.Component
    {
        public CsvBulkLoader()
        {
            InitializeComponent();
        }

        public CsvBulkLoader(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
       // private const string stagingTable = "[dbo].[India_pincode_Staging]";

        public static async Task<Guid> BulkLoadCsvToStagingAndProcessAsync(string csvPath, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(csvPath)) throw new ArgumentNullException(nameof(csvPath));
            if (!File.Exists(csvPath)) throw new FileNotFoundException("CSV file not found.", csvPath);
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            string stagingTable = "[dbo].[India_pincode_Staging]";
            var importBatchId = Guid.NewGuid();

            // read CSV header and data into DataTable
            DataTable table = new DataTable("Staging");
            using (var sr = new StreamReader(csvPath, Encoding.UTF8))
            {
                string? headerLine = await sr.ReadLineAsync();
                if (headerLine == null) throw new InvalidOperationException("CSV is empty or unreadable.");

                var headers = ParseCsvLine(headerLine);
                for (int i = 0; i < headers.Length; i++)
                {
                    string colName = string.IsNullOrWhiteSpace(headers[i]) ? $"Column{i + 1}" : headers[i].Trim();
                    // normalize column names for SQL (keep original header as DataColumn.ColumnName)
                    table.Columns.Add(colName, typeof(string));
                }

                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var fields = ParseCsvLine(line);
                    var row = table.NewRow();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i < fields.Length)
                        {
                            var v = fields[i];
                            row[i] = string.IsNullOrWhiteSpace(v) ? (object)DBNull.Value : v.Trim();
                        }
                        else
                        {
                            row[i] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // create staging table if not exists, or truncate if exists
                var createOrTruncate = BuildCreateOrTruncateStatement(stagingTable, table.Columns);
                using (var cmd = new SqlCommand(createOrTruncate, conn))
                {
                    cmd.CommandTimeout = 120;
                    await cmd.ExecuteNonQueryAsync();
                }

                // perform bulk copy
                using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, null))
                {
                    bulk.DestinationTableName = stagingTable;
                    bulk.BatchSize = 5000;
                    bulk.BulkCopyTimeout = 600;

                    // map columns
                    foreach (DataColumn col in table.Columns)
                        bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                    await bulk.WriteToServerAsync(table);
                }

                // Optional: call stored procedure to process staging if it exists
                string procName = "dbo.ProcessImportedPincodes";
                if (await StoredProcedureExistsAsync(conn, procName))
                {
                    using (var proc = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        proc.Parameters.Add(new SqlParameter("@ImportBatchId", System.Data.SqlDbType.UniqueIdentifier) { Value = importBatchId });
                        proc.Parameters.Add(new SqlParameter("@StagingTableName", System.Data.SqlDbType.NVarChar, 200) { Value = stagingTable });
                        proc.CommandTimeout = 600;
                        await proc.ExecuteNonQueryAsync();
                    }
                }
            }

            return importBatchId;
        }

        // Wraps existing implementation, logs exceptions, returns structured ImportResult
        public static async Task<ImportResult> BulkLoadCsvToStagingAndProcessWithResultAsync(
            string csvPath,
            string connectionString,
            CancellationToken cancellationToken = default,
            IProgress<int>? progress = null)
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logDir);
            string logFile = Path.Combine(logDir, $"import_{DateTime.Now:yyyyMMdd_HHmmss}.log");

            try
            {
                // quick pre-checks
                if (string.IsNullOrWhiteSpace(csvPath) || !File.Exists(csvPath))
                    throw new FileNotFoundException("CSV file not found.", csvPath);

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentException("Connection string is required.", nameof(connectionString));

                // If your existing method supports cancellation/progress, prefer calling it.
                // Otherwise call the existing Guid-returning method and capture the id.
                Guid batchId = await BulkLoadCsvToStagingAndProcessAsync(csvPath, connectionString);

                // Optionally parse summary from DB or stored-proc results here to fill RowsImported/RowsUpdated.
                // For now we return success with batchId.
                File.AppendAllText(logFile, $"[{DateTime.UtcNow}] Import succeeded. BatchId={batchId}{Environment.NewLine}");
                return new ImportResult
                {
                    Success = true,
                    ImportBatchId = batchId,
                    LogFilePath = logFile
                };
            }
            catch (OperationCanceledException)
            {
                File.AppendAllText(logFile, $"[{DateTime.UtcNow}] Import cancelled.{Environment.NewLine}");
                return new ImportResult { Success = false, Error = new OperationCanceledException(), LogFilePath = logFile };
            }
            catch (Exception ex)
            {
                // write full details to log for later inspection
                File.AppendAllText(logFile, $"[{DateTime.UtcNow}] Import failed: {ex}{Environment.NewLine}");
                return new ImportResult { Success = false, Error = ex, LogFilePath = logFile };
            }
        }

        private static async Task<bool> StoredProcedureExistsAsync(SqlConnection conn, string procFullName)
        {
            if (conn == null) throw new ArgumentNullException(nameof(conn));
            if (string.IsNullOrWhiteSpace(procFullName)) return false;

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(@name) AND type = 'P'", conn))
            {
                cmd.Parameters.AddWithValue("@name", procFullName);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        private static string BuildCreateOrTruncateStatement(string stagingTable, DataColumnCollection columns)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"IF OBJECT_ID(N'{stagingTable}', N'U') IS NULL");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    CREATE TABLE {stagingTable} (");

            var colDefs = new List<string>();
            foreach (DataColumn col in columns)
            {
                string colNameEscaped = EscapeSqlIdentifier(col.ColumnName);
                // use nvarchar(max) for simplicity; adjust if you have a fixed schema
                colDefs.Add($"    {colNameEscaped} NVARCHAR(MAX) NULL");
            }

            sb.AppendLine(string.Join(",\n", colDefs));
            sb.AppendLine("    );");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    TRUNCATE TABLE {stagingTable};");
            sb.AppendLine("END");
            return sb.ToString();
        }

        private static string EscapeSqlIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name)) return "[Column]";
            return "[" + name.Replace("]", "]]") + "]";
        }

        // Basic CSV parser that supports quoted fields with double-quote escaping
        private static string[] ParseCsvLine(string line)
        {
            var fields = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // escaped quote
                        sb.Append('"');
                        i++; // skip next
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            fields.Add(sb.ToString());
            return fields.ToArray();
        }
    }
}
