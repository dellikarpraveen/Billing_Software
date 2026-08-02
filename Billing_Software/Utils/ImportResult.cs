#nullable enable
using System;

namespace Billing_Software.Utils
{
    public sealed class ImportResult
    {
        public bool Success { get; set; }
        public Guid? ImportBatchId { get; set; }
        public Exception? Error { get; set; }
        public string? LogFilePath { get; set; }
        public int? RowsImported { get; set; }
        public int? RowsUpdated { get; set; }
        public override string ToString()
        {
            if (Success) return $"Success: BatchId={ImportBatchId} RowsInserted={RowsImported} RowsUpdated={RowsUpdated}";
            return $"Failed: {Error?.Message}";
        }
    }
}
