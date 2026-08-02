#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using OfficeOpenXml; // Required for EPPlus

namespace Billing_Software
{

    public class AccountingImportEngine
    {
        private readonly string _connectionString = "BillingSoftwareDB";

        // Dictionary to cache Group Names and their database IDs
        private Dictionary<string, int> FetchGroupCache()
        {
            var cache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            string query = "SELECT GroupID, GroupName FROM AccountGroups;";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cache.Add(reader["GroupName"].ToString().Trim(), Convert.ToInt32(reader["GroupID"]));
                        }
                    }
                }
            }
            return cache;
        }

        // Method to import data from Excel or CSV
        public void ImportLedgersFromFile(string filePath)
        {
            // Ensure EPPlus is configured for non-commercial use. Use helper to support multiple EPPlus versions.
            Billing_Software.Helpers.EpplusLicenseHelper.SetNonCommercial();

            // 1. Fetch current Group IDs from database to map text to integers
            var groupCache = FetchGroupCache();

            // 2. Prepare DataTable structural template matching the SQL Destination Table
            DataTable importTable = new DataTable();
            importTable.Columns.Add("GroupID", typeof(int));
            importTable.Columns.Add("LedgerName", typeof(string));
            importTable.Columns.Add("OpeningBalance", typeof(decimal));
            importTable.Columns.Add("BalanceType", typeof(string));
            importTable.Columns.Add("IsActive", typeof(bool));

            var fileInfo = new FileInfo(filePath);

            // 3. Open and Parse File Content
            using (var package = new ExcelPackage(fileInfo))
            {
                // Works for single-sheet Excel files or CSV files natively opened via EPPlus streams
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // Start loop at Row 2 to bypass your text headers
                for (int row = 2; row <= rowCount; row++)
                {
                    string? ledgerName = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                    string? groupName = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
                    string? opBalanceStr = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                    string? balanceType = worksheet.Cells[row, 4].Value?.ToString()?.Trim()?.ToUpper();

                    // Skip blank or corrupt file rows gracefully
                    if (string.IsNullOrEmpty(ledgerName) || string.IsNullOrEmpty(groupName)) continue;

                    // Validate if Group Name in file accurately matches database configuration
                    if (!string.IsNullOrEmpty(groupName))
                    {
                        var groupNameKey = groupName!; // verified non-null above
                        if (groupCache.TryGetValue(groupNameKey, out int groupId))
                        {
                        decimal openingBalance = decimal.TryParse(opBalanceStr, out decimal parsedBal) ? parsedBal : 0.00m;

                        // Fallback validation for balance types
                        if (balanceType != "DR" && balanceType != "CR") balanceType = "DR";

                        var ledgerNameSafe = ledgerName ?? string.Empty;
                        var balanceTypeSafe = balanceType ?? "DR";

                        // Append standardized row to memory table array (use DBNull for nulls if needed)
                        importTable.Rows.Add(groupId, ledgerNameSafe, openingBalance, balanceTypeSafe, true);
                        }
                        else
                        {
                            // Optional Logging: Handle unmapped group warnings here
                            Console.WriteLine($"Warning: Group '{groupName}' not found in database. Skipping row {row}.");
                        }
                    }
                }
            }

            // 4. Stream Memory Array straight to your SQL Production server
            if (importTable.Rows.Count > 0)
            {
                ExecuteBulkInsert(importTable);
                Console.WriteLine($"Success: {importTable.Rows.Count} Ledgers imported successfully.");
            }
        }

        private void ExecuteBulkInsert(DataTable dataToInsert)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = "LedgerMasters";

                    // Map your system fields clearly
                    bulkCopy.ColumnMappings.Add("GroupID", "GroupID");
                    bulkCopy.ColumnMappings.Add("LedgerName", "LedgerName");
                    bulkCopy.ColumnMappings.Add("OpeningBalance", "OpeningBalance");
                    bulkCopy.ColumnMappings.Add("BalanceType", "BalanceType");
                    bulkCopy.ColumnMappings.Add("IsActive", "IsActive");

                    bulkCopy.WriteToServer(dataToInsert);
                }
            }
        }
    }

}
