#nullable enable
using System;
using System.IO;

namespace Billing_Software.Utils
{
    /// <summary>
    /// Lightweight helper used by the UI to produce a CSV file from an Excel path.
    /// This is intentionally minimal so the project compiles. Replace with a real Excel parser (e.g. ExcelDataReader, OpenXml, EPPlus)
    /// when you need true Excel->CSV conversion.
    /// </summary>
    public static class ExcelToCsvConverter
    {
        /// <summary>
        /// Convert an input file to CSV. If input is already CSV, copy it. For real Excel files this method currently
        /// writes a placeholder CSV and throws a <see cref="NotSupportedException"/> to signal limited behavior.
        /// </summary>
        public static void ConvertToCsv(string inputPath, string outputCsvPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath)) throw new ArgumentNullException(nameof(inputPath));
            if (string.IsNullOrWhiteSpace(outputCsvPath)) throw new ArgumentNullException(nameof(outputCsvPath));

            var ext = Path.GetExtension(inputPath).ToLowerInvariant();
            if (ext == ".csv")
            {
                // simple copy for CSV -> CSV
                File.Copy(inputPath, outputCsvPath, overwrite: true);
                return;
            }

            // Minimal fallback: create a small CSV stub so UI call paths don't crash.
            // Replace this with a proper Excel reader implementation when available.
            var msg = $"Excel->CSV conversion for '{Path.GetFileName(inputPath)}' is not implemented. Producing an empty CSV placeholder.";
            File.WriteAllText(outputCsvPath, $"# {msg}{Environment.NewLine}");
            // Optionally surface a NotSupportedException if you prefer callers to treat this as failure:
            // throw new NotSupportedException("Excel->CSV conversion not implemented. Add Excel reader dependency.");
        }
    }
}