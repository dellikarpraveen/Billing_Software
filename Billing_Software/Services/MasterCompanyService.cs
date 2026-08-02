#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Linq;

namespace Billing_Software.Services
{
    public static class MasterCompanyService
    {
        public class ProgressReport
        {
            public int Completed { get; set; }
            public int Total { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        /// <summary>
        /// Parse CSV into GroupDefinition list and return any validation errors.
        /// This does not access the database.
        /// </summary>
        public static (List<GroupDefinition> Rows, List<string> Errors) ParseGroupsFromCsv(string csvPath)
        {
            var rows = new List<GroupDefinition>();
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(csvPath))
            {
                errors.Add("csvPath is empty");
                return (rows, errors);
            }
            if (!File.Exists(csvPath))
            {
                errors.Add($"CSV file not found: {csvPath}");
                return (rows, errors);
            }

            var lines = File.ReadAllLines(csvPath);
            int lineNo = 0;
            foreach (var raw in lines)
            {
                lineNo++;
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var parts = raw.Split(new[] { ',', '\t' }, StringSplitOptions.None).Select(p => p.Trim()).ToArray();
                // Skip header if first token is non-numeric 'GroupID' or 'GroupName'
                if (rows.Count == 0 && (parts.Length == 0 || string.Equals(parts[0], "GroupID", StringComparison.OrdinalIgnoreCase) || string.Equals(parts[0], "GroupName", StringComparison.OrdinalIgnoreCase)))
                    continue;

                int? sid = null;
                if (parts.Length > 0 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var p)) sid = p;
                var name = parts.Length > 1 ? parts[1] : string.Empty;
                int? parentSid = null;
                if (parts.Length > 2 && int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var ps)) parentSid = ps;
                var gtype = parts.Length > 3 ? parts[3] : string.Empty;
                string? ledger = parts.Length > 4 ? parts[4] : null;
                bool? createLedger = null;
                if (parts.Length > 5)
                {
                    var v = parts[5];
                    if (!string.IsNullOrWhiteSpace(v))
                    {
                        if (bool.TryParse(v, out var bv)) createLedger = bv;
                        else if (v == "1" || v.Equals("yes", StringComparison.OrdinalIgnoreCase) || v.Equals("y", StringComparison.OrdinalIgnoreCase)) createLedger = true;
                        else if (v == "0" || v.Equals("no", StringComparison.OrdinalIgnoreCase) || v.Equals("n", StringComparison.OrdinalIgnoreCase)) createLedger = false;
                    }
                }

                if (string.IsNullOrEmpty(name))
                {
                    errors.Add($"Line {lineNo}: GroupName is required.");
                }
                if (string.IsNullOrEmpty(gtype))
                {
                    errors.Add($"Line {lineNo}: GroupType is recommended (empty allowed).\n");
                }

                rows.Add(new GroupDefinition { SourceId = sid, GroupName = name, ParentSourceId = parentSid, GroupType = gtype, LedgerName = ledger, CreateLedger = createLedger });
            }

            // Basic duplicate source id check
            var dupes = rows.Where(r => r.SourceId.HasValue).GroupBy(r => r.SourceId.Value).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (dupes.Any()) errors.Add("Duplicate GroupID values found: " + string.Join(",", dupes));

            return (rows, errors);
        }

        /// <summary>
        /// Re-validate a list of group rows (used after inline edits in preview).
        /// </summary>
        public static List<string> ValidateParsedRows(IEnumerable<GroupDefinition> rows)
        {
            var list = rows.ToList();
            var errors = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                var r = list[i];
                if (string.IsNullOrWhiteSpace(r.GroupName)) errors.Add($"Row {r.SourceLineNumber ?? (i + 1)}: GroupName is required.");
                if (string.IsNullOrWhiteSpace(r.GroupType)) errors.Add($"Row {r.SourceLineNumber ?? (i + 1)}: GroupType is recommended.");
            }

            // duplicate source id
            var dupes = list.Where(r => r.SourceId.HasValue).GroupBy(r => r.SourceId.Value).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (dupes.Any()) errors.Add("Duplicate GroupID values found: " + string.Join(",", dupes));

            // duplicate name+type
            var dupNames = list.GroupBy(r => (r.GroupName ?? string.Empty).Trim().ToLowerInvariant() + "|" + (r.GroupType ?? string.Empty).Trim().ToLowerInvariant()).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (dupNames.Any()) errors.Add("Duplicate GroupName+GroupType combinations found: " + string.Join(",", dupNames));

            // parent reference existence
                var definedIds = new HashSet<int>(list.Where(r => r.SourceId.HasValue).Select(r => r.SourceId.GetValueOrDefault()));
            var badParents = list.Where(r => r.ParentSourceId.HasValue && !definedIds.Contains(r.ParentSourceId.Value)).Select(r => r.SourceLineNumber ?? -1).ToList();
            if (badParents.Any()) errors.Add("Rows with ParentGroupID referencing unknown GroupID: lines " + string.Join(",", badParents));

            return errors;
        }
        /// <summary>
        /// Represents a group row coming from CSV or inline definitions.
        /// </summary>
        public class GroupDefinition
        {
            public int? SourceId { get; set; }
            // Original CSV line number for reference in previews and errors
            public int? SourceLineNumber { get; set; }
            public string GroupName { get; set; } = string.Empty;
            public int? ParentSourceId { get; set; }
            public string? ParentGroupName { get; set; }
            public string GroupType { get; set; } = string.Empty;
            // Optional explicit ledger name to create for this group
            public string? LedgerName { get; set; }
            // If true, create ledger for this row even if createLedgersForAllGroups is false
            public bool? CreateLedger { get; set; }
        }

        /// <summary>
        /// Import groups from a CSV file and seed the account groups and optional default ledgers.
        /// Expected CSV columns (header optional): GroupID,GroupName,ParentGroupID,GroupType
        /// ParentGroupID refers to the source GroupID in the CSV. ParentGroupName will be resolved automatically.
        /// </summary>
        public static void ImportGroupsFromCsv(string connectionString, string csvPath, bool createLedgersForAllGroups = true, bool dryRun = false, IProgress<ProgressReport>? progress = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(csvPath)) throw new ArgumentNullException(nameof(csvPath));
            if (!File.Exists(csvPath)) throw new FileNotFoundException("CSV not found", csvPath);

            var lines = File.ReadAllLines(csvPath);
            var rows = new List<GroupDefinition>();

            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var parts = raw.Split(new[] { ',', '\t' }, StringSplitOptions.None).Select(p => p.Trim()).ToArray();
                // Skip header if first token is non-numeric 'GroupID' or 'GroupName'
                if (rows.Count == 0 && (parts.Length == 0 || string.Equals(parts[0], "GroupID", StringComparison.OrdinalIgnoreCase) || string.Equals(parts[0], "GroupName", StringComparison.OrdinalIgnoreCase)))
                    continue;

                // Expect at least 4 columns: GroupID,GroupName,ParentGroupID,GroupType
                // Optional columns: LedgerName, CreateLedger (true/false or 1/0)
                int? sid = null;
                if (parts.Length > 0 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var p)) sid = p;
                var name = parts.Length > 1 ? parts[1] : string.Empty;
                int? parentSid = null;
                if (parts.Length > 2 && int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var ps)) parentSid = ps;
                var gtype = parts.Length > 3 ? parts[3] : string.Empty;
                string? ledger = parts.Length > 4 ? parts[4] : null;
                bool? createLedger = null;
                if (parts.Length > 5)
                {
                    var v = parts[5];
                    if (!string.IsNullOrWhiteSpace(v))
                    {
                        if (bool.TryParse(v, out var bv)) createLedger = bv;
                        else if (v == "1" || v.Equals("yes", StringComparison.OrdinalIgnoreCase) || v.Equals("y", StringComparison.OrdinalIgnoreCase)) createLedger = true;
                        else if (v == "0" || v.Equals("no", StringComparison.OrdinalIgnoreCase) || v.Equals("n", StringComparison.OrdinalIgnoreCase)) createLedger = false;
                    }
                }

                rows.Add(new GroupDefinition { SourceId = sid, GroupName = name, ParentSourceId = parentSid, GroupType = gtype, LedgerName = ledger, CreateLedger = createLedger });
            }


            cancellationToken.ThrowIfCancellationRequested();

            // Resolve ParentGroupName by SourceId lookup
            var bySource = rows.Where(r => r.SourceId.HasValue).ToDictionary(r => r.SourceId.GetValueOrDefault(), r => r);
            foreach (var r in rows)
            {
                if (r.ParentSourceId.HasValue && bySource.TryGetValue(r.ParentSourceId.Value, out var parent))
                    r.ParentGroupName = parent.GroupName;
            }
            

            // Seed to DB (pass through dryRun and progress)
            CreateOrUpdateGroupsAndLedgers(connectionString, rows, createLedgersForAllGroups, dryRun, progress, cancellationToken);
        }

        /// <summary>
        /// Create or update given groups and optionally create default ledgers for them.
        /// Upsert by GroupName + GroupType and resolve parent relationships by GroupName.
        /// </summary>
        public static void CreateOrUpdateGroupsAndLedgers(string connectionString, IEnumerable<GroupDefinition> groups, bool createLedgersForAllGroups = true, bool dryRun = false, IProgress<ProgressReport>? progress = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            var list = groups.ToList();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Process roots first (no parent), then iterative for children
                        var processed = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); // key: GroupName|GroupType -> GroupID

                        // Helper to build key
                        string Key(GroupDefinition g) => (g.GroupName ?? string.Empty) + "|" + (g.GroupType ?? string.Empty);

                        // Keep processing until all groups handled or no progress
                        var remaining = new HashSet<GroupDefinition>(list);
                        bool progressed;
                        do
                        {
                            progressed = false;
                            foreach (var g in remaining.ToList())
                            {
                                // If has parent, only process after parent processed (or no parent)
                                if (!string.IsNullOrEmpty(g.ParentGroupName))
                                {
                                    var parentKey = (g.ParentGroupName ?? string.Empty) + "|" + (g.GroupType ?? string.Empty);
                                    if (!processed.ContainsKey(parentKey)) continue; // wait
                                }

                                // Ensure group exists and get DB id
                                int dbGroupId = UpsertGroup(conn, tx, g.GroupName ?? string.Empty, g.GroupType ?? string.Empty, g.ParentGroupName == null ? (int?)null : ResolveParentDbId(processed, g.ParentGroupName ?? string.Empty, g.GroupType));
                                processed[Key(g)] = dbGroupId;

                                // Optionally create default ledger(s)
                                if (createLedgersForAllGroups)
                                {
                                    var ledgerName = PickDefaultLedgerName(g.GroupName);
                                    EnsureLedgerExists(conn, tx, dbGroupId, ledgerName);
                                }

                                remaining.Remove(g);
                                progressed = true;
                            }
                        } while (progressed && remaining.Count > 0);

            // If still remaining (circular or unresolved), process them without parent resolution
            foreach (var g in remaining)
            {
                var gName = g.GroupName ?? string.Empty;
                var gType = g.GroupType ?? string.Empty;
                int dbGroupId = UpsertGroup(conn, tx, gName, gType, null);
                if (createLedgersForAllGroups) EnsureLedgerExists(conn, tx, dbGroupId, PickDefaultLedgerName(gName));
            }

                        tx.Commit();
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        private static int ResolveParentDbId(Dictionary<string, int> processed, string parentGroupName, string? groupType)
        {
            var key = parentGroupName + "|" + (groupType ?? string.Empty);
            if (processed.TryGetValue(key, out var id)) return id;
            // fallback: try without type
            var alt = processed.FirstOrDefault(kv => string.Equals(kv.Key.Split('|')[0], parentGroupName, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(alt.Key)) return alt.Value;
            return -1;
        }

        private static string PickDefaultLedgerName(string groupName)
        {
            // Normalize input
            if (string.IsNullOrWhiteSpace(groupName)) return "Ledger";
            var gn = groupName.Trim();

            // Exact mappings for common groups
            var exact = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Assets", "Assets Ledger" },
                { "Liabilities", "Liabilities Ledger" },
                { "Equity", "Capital Account" },
                { "Income", "Income Ledger" },
                { "Expenses", "Expenses Ledger" },
                { "Capital Account", "Capital Account" },
                { "Current Assets", "Current Assets Ledger" },
                { "Current Liabilities", "Current Liabilities Ledger" },
                { "Fixed Assets", "Fixed Assets Ledger" },
                { "Investments", "Investments Ledger" },
                { "Direct Expenses", "Direct Expenses Ledger" },
                { "Direct Income", "Direct Income Ledger" },
                { "Purchase Account", "Purchase Ledger" },
                { "Sales Account", "Sales Ledger" },
                { "Suspense Account", "Suspense Ledger" },
                { "Bank Accounts", "Bank Account" },
                { "Deposit (Assets)", "Deposits Ledger" },
                { "Stock in Hand", "Stock in Hand Ledger" },
                { "Sundry Debtors", "Sundry Debtors Ledger" },
                { "Bank OD Account", "Bank OD Ledger" },
                { "Duties and Taxes", "Duties and Taxes Ledger" },
                { "Provisions", "Provisions Ledger" },
                { "Sundry Creditors", "Sundry Creditors Ledger" }
            };

            if (exact.TryGetValue(gn, out var mapped)) return mapped;

            // Fallback heuristics
            var lower = gn.ToLowerInvariant();
            if (lower.Contains("sale")) return "Sales Ledger";
            if (lower.Contains("purcha")) return "Purchase Ledger";
            if (lower.Contains("capital")) return "Capital Account";
            if (lower.Contains("bank")) return "Bank Account";
            if (lower.Contains("expense")) return "General Expenses";
            if (lower.Contains("income") || lower.Contains("revenue")) return "Other Income";

            return gn + " Ledger";
        }

        private static int UpsertGroup(SqlConnection conn, SqlTransaction tx, string groupName, string groupType, int? parentGroupId, bool dryRun = false, IProgress<ProgressReport>? progress = null, CancellationToken cancellationToken = default)
        {
            // Try find existing row by GroupName + GroupType
            using (var cmd = new SqlCommand("SELECT TOP(1) GroupID, ParentGroupID FROM AccountGroups WHERE GroupName = @name AND GroupType = @type", conn, tx))
            {
                cmd.Parameters.AddWithValue("@name", groupName);
                cmd.Parameters.AddWithValue("@type", groupType ?? string.Empty);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        int id = Convert.ToInt32(rdr[0]);
                        var existingParent = rdr.IsDBNull(1) ? (int?)null : Convert.ToInt32(rdr[1]);
                        rdr.Close();
                        // update parent if different
                        if (parentGroupId.HasValue && existingParent != parentGroupId.Value)
                        {
                            progress?.Report(new ProgressReport { Completed = 0, Total = 0, Message = $"Would update parent for group '{groupName}' -> set ParentGroupID={parentGroupId}" });
                            if (!dryRun)
                            {
                                using (var u = new SqlCommand("UPDATE AccountGroups SET ParentGroupID = @pid WHERE GroupID = @id", conn, tx))
                                {
                                    u.Parameters.AddWithValue("@pid", parentGroupId.Value);
                                    u.Parameters.AddWithValue("@id", id);
                                    u.ExecuteNonQuery();
                                }
                            }
                        }
                        return id;
                    }
                }   
            }

            // Insert new group
            progress?.Report(new ProgressReport { Completed = 0, Total = 0, Message = $"Would insert group '{groupName}' (Type={groupType}) with ParentGroupID={parentGroupId}" });
            if (dryRun)
            {
                // Return a deterministic negative id for dry-run simulation
                var fake = -Math.Abs(groupName.GetHashCode());
                return fake == 0 ? -1 : fake;
            }
            using (var cmd = new SqlCommand("INSERT INTO AccountGroups (GroupName, ParentGroupID, GroupType) OUTPUT INSERTED.GroupID VALUES (@name, @pid, @type)", conn, tx))
            {
                cmd.Parameters.AddWithValue("@name", groupName);
                if (parentGroupId.HasValue) cmd.Parameters.AddWithValue("@pid", parentGroupId.Value); else cmd.Parameters.AddWithValue("@pid", DBNull.Value);
                cmd.Parameters.AddWithValue("@type", groupType ?? string.Empty);
                var id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        private static void EnsureLedgerExists(SqlConnection conn, SqlTransaction tx, int groupId, string ledgerName, bool dryRun = false, IProgress<ProgressReport>? progress = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var cmd = new SqlCommand("SELECT TOP(1) LedgerID FROM LedgerMasters WHERE GroupID = @gid AND LedgerName = @lname", conn, tx))
            {
                cmd.Parameters.AddWithValue("@gid", groupId);
                cmd.Parameters.AddWithValue("@lname", ledgerName);
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value) return; // exists
            }

            progress?.Report(new ProgressReport { Completed = 0, Total = 0, Message = $"Would create ledger '{ledgerName}' for GroupID={groupId}" });
            if (dryRun) return;
            using (var cmd = new SqlCommand("INSERT INTO LedgerMasters (GroupID, LedgerName, OpeningBalance, BalanceType, IsActive) VALUES (@gid, @lname, @bal, @btype, @active)", conn, tx))
            {
                cmd.Parameters.AddWithValue("@gid", groupId);
                cmd.Parameters.AddWithValue("@lname", ledgerName);
                cmd.Parameters.AddWithValue("@bal", 0.00m);
                cmd.Parameters.AddWithValue("@btype", "DR");
                cmd.Parameters.AddWithValue("@active", true);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        // Inserts account groups hierarchically. Ensures parents are created first to correctly map database FKs.
        /// </summary>
        private static void InsertHierarchicalGroups(string connectionString, List<GroupDefinition> rows, bool createLedgersForAllGroups)
        {
            // Dictionary to map: SourceId (from file) -> Newly Generated Database Identity ID (from SQL Server)
            var idMapping = new Dictionary<int, int>();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Wrap in a transaction so if one row fails, the whole file rolls back cleanly
            using var transaction = connection.BeginTransaction();

            try
            {
                // Keep looping until all rows are inserted or no progress can be made
                var remainingRows = new List<GroupDefinition>(rows);
                int lastCount = -1;

                while (remainingRows.Count > 0)
                {
                    // Safeguard against infinite loops if there is an unhandled circular dependency
                    if (remainingRows.Count == lastCount)
                    {
                        throw new InvalidOperationException("Circular reference or unresolvable hierarchy detected in the dataset.");
                    }
                    lastCount = remainingRows.Count;

                    // Find rows ready for insertion: Either they have no parent, or their parent has already been inserted
                    var readyToInsert = remainingRows
                        .Where(r => !r.ParentSourceId.HasValue || idMapping.ContainsKey(r.ParentSourceId.Value))
                        .ToList();

                    foreach (var row in readyToInsert)
                    {
                        // 1. Resolve Parent Group ID from our runtime tracking dictionary
                        int? dbParentId = null;
                        if (row.ParentSourceId.HasValue)
                        {
                            dbParentId = idMapping[row.ParentSourceId.Value];
                        }

                        // 2. Insert the Account Group into the Database and capture its generated Identity ID
                        const string groupQuery = @"
                    INSERT INTO AccountGroups (GroupName, ParentGroupID, GroupType) 
                    VALUES (@GroupName, @ParentGroupID, @GroupType);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                        int generatedGroupId;
                        using (var cmd = new SqlCommand(groupQuery, connection, transaction))
                        {
                            cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 100).Value = row.GroupName;
                            cmd.Parameters.Add("@ParentGroupID", SqlDbType.Int).Value = (object?)dbParentId ?? DBNull.Value;
                            cmd.Parameters.Add("@GroupType", SqlDbType.VarChar, 50).Value = (object?)row.GroupType ?? DBNull.Value;

                            generatedGroupId = (int)cmd.ExecuteScalar();
                        }

                        // Map this row's file ID to its new database identity ID
                        if (row.SourceId.HasValue)
                        {
                            idMapping[row.SourceId.Value] = generatedGroupId;
                        }

                        // 3. Optional: Business logic handling for automatic ledger generation
                        bool determineLedgerCreation = row.CreateLedger ?? createLedgersForAllGroups;
                        if (determineLedgerCreation)
                        {
                            string finalLedgerName = !string.IsNullOrWhiteSpace(row.LedgerName) ? row.LedgerName : row.GroupName;

                            const string ledgerQuery = @"
                        INSERT INTO Ledgers (LedgerName, AccountGroupID) 
                        VALUES (@LedgerName, @AccountGroupID);";

                            using var ledgerCmd = new SqlCommand(ledgerQuery, connection, transaction);
                            ledgerCmd.Parameters.Add("@LedgerName", SqlDbType.VarChar, 100).Value = finalLedgerName;
                            ledgerCmd.Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = generatedGroupId;
                            ledgerCmd.ExecuteNonQuery();
                        }

                        // Remove from processing pool
                        remainingRows.Remove(row);
                    }
                }

                // Everything succeeded, safe to commit the transaction
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // Escalate error out to UI handler
            }
        }
        // ... Keep your ParseGroupsFromCsv and GroupDefinition code exactly as it is ...

        /// <summary>
        /// Creates a new company record in SQL and automatically initializes it with default account groups.
        /// </summary>
        public static void CreateNewCompany(string connectionString, string companyName, string databaseTemplatePath)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name cannot be empty.", nameof(companyName));
            if (!File.Exists(databaseTemplatePath)) throw new FileNotFoundException("Default account groups template file not found.", databaseTemplatePath);

            // 1. Parse your tab-separated master structure file first
            var (rows, errors) = ParseGroupsFromCsv(databaseTemplatePath);
            if (errors.Count > 0 && errors.Exists(e => !e.Contains("recommended")))
            {
                throw new InvalidDataException("Template file contains errors and cannot be used to seed the new company.");
            }

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 2. Insert the brand new company record
                const string companyQuery = @"
                INSERT INTO Companies (CompanyName, CreatedAt) 
                VALUES (@CompanyName, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int);";

                int newCompanyId;
                using (var cmd = new SqlCommand(companyQuery, connection, transaction))
                {
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 150).Value = companyName.Trim();
                    newCompanyId = (int)cmd.ExecuteScalar();
                }

                // 3. Hierarchically seed the Account Groups tied to this specific Company ID
                var idMapping = new Dictionary<int, int>(); // Maps File ID -> New DB ID
                var remainingRows = new List<GroupDefinition>(rows);
                int lastCount = -1;

                while (remainingRows.Count > 0)
                {
                    if (remainingRows.Count == lastCount)
                    {
                        throw new InvalidOperationException("Circular reference breakdown detected in account structure template.");
                    }
                    lastCount = remainingRows.Count;

                    var readyToInsert = remainingRows
                        .Where(r => !r.ParentSourceId.HasValue || idMapping.ContainsKey(r.ParentSourceId.Value))
                        .ToList();

                    foreach (var row in readyToInsert)
                    {
                        int? dbParentId = row.ParentSourceId.HasValue ? idMapping[row.ParentSourceId.Value] : null;

                        // Modified to include CompanyID column so groups belong strictly to this new company
                        const string groupQuery = @"
                        INSERT INTO AccountGroups (CompanyNameId, GroupName, ParentGroupID, GroupType) 
                        VALUES (@CompanyID, @GroupName, @ParentGroupID, @GroupType);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                        int generatedGroupId;
                        using (var cmd = new SqlCommand(groupQuery, connection, transaction))
                        {
                            cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = newCompanyId;
                            cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 100).Value = row.GroupName;
                            cmd.Parameters.Add("@ParentGroupID", SqlDbType.Int).Value = (object?)dbParentId ?? DBNull.Value;
                            cmd.Parameters.Add("@GroupType", SqlDbType.VarChar, 50).Value = (object?)row.GroupType ?? DBNull.Value;

                            generatedGroupId = (int)cmd.ExecuteScalar();
                        }

                        if (row.SourceId.HasValue)
                        {
                            idMapping[row.SourceId.Value] = generatedGroupId;
                        }

                        remainingRows.Remove(row);
                    }
                }

                // All updates verified successfully; lock records live into SQL Server
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // Pass up to the UI Form layer to show in a MessageBox
            }
        }
    }




}

