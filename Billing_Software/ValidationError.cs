using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_Software
{
    public class ValidationError
    {
        public int RowIndex { get; set; }
        public string LedgerName { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string Criticality { get; set; } = string.Empty; // "CRITICAL" or "WARNING"
    }

    public class ImportResultSummary
    {
        public int TotalRowsProcessed { get; set; }
        public int SuccessfullyImported { get; set; }
        public int SkippedDuplicates { get; set; }
        public List<ValidationError> ErrorLogs { get; set; } = new List<ValidationError>();
    }

}
