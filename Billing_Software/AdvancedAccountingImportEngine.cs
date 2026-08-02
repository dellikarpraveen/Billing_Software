using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Billing_Software
{
    public static class AdvancedAccountingImportEngine
    {
        // ... Keep any class properties or structures you have here ...

        public static (List<Billing_Software.Services.MasterCompanyService.GroupDefinition> Rows, List<string> Errors) ParseGroupsFromCsv(string csvPath)
        {
            var rows = new List<Billing_Software.Services.MasterCompanyService.GroupDefinition>();
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

            // Read the file data into an array of lines
            string[] fileLines = File.ReadAllLines(csvPath);
            int lineNo = 0;
            bool isFirstRow = true;

            foreach (string rawLine in fileLines)
            {
                lineNo++;
                if (string.IsNullOrWhiteSpace(rawLine)) continue;

                // Split on Tab (\t) for TSV formatting alignment
                string[] parts = rawLine.Split('\t').Select(p => p.Trim()).ToArray();

                // Explicitly check for headers only on the first non-empty text row
                if (isFirstRow)
                {
                    isFirstRow = false;
                    if (parts.Length > 0 && (string.Equals(parts[0], "GroupID", StringComparison.OrdinalIgnoreCase) ||
                                             string.Equals(parts[0], "GroupName", StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }
                }

                bool hasError = false;

                // 1. Group ID (SourceId)
                int? sid = null;
                if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
                {
                    if (int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var p)) sid = p;
                    else
                    {
                        errors.Add($"Line {lineNo}: SourceId '{parts[0]}' must be an integer.");
                        hasError = true;
                    }
                }

                // 2. Group Name
                var name = parts.Length > 1 ? parts[1] : string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    errors.Add($"Line {lineNo}: GroupName is required.");
                    hasError = true;
                }

                // 3. Parent Group ID (ParentSourceId) - Safely skips "NULL" string values
                int? parentSid = null;
                if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]) && !string.Equals(parts[2], "NULL", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var ps)) parentSid = ps;
                    else
                    {
                        errors.Add($"Line {lineNo}: ParentSourceId '{parts[2]}' must be an integer or NULL.");
                        hasError = true;
                    }
                }

                // 4. Group Type
                var gtype = parts.Length > 3 ? parts[3] : string.Empty;

                // 5. Ledger Name (Optional field - falls back to null gracefully if missing)
                string? ledger = parts.Length > 4 && !string.IsNullOrWhiteSpace(parts[4]) ? parts[4] : null;

                // 6. Create Ledger Flag (Optional field - falls back to null gracefully if missing)
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

                if (!hasError)
                {
                    rows.Add(new Billing_Software.Services.MasterCompanyService.GroupDefinition
                    {
                        SourceId = sid,
                        SourceLineNumber = lineNo,
                        GroupName = name,
                        ParentSourceId = parentSid,
                        GroupType = gtype,
                        LedgerName = ledger,
                        CreateLedger = createLedger
                    });
                }
            }

            return (rows, errors);
        }
    }
}
