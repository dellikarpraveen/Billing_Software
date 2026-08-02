#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Billing_Software.Utils
{
    /// <summary>
    /// Result of header validation.
    /// </summary>
    public sealed class HeaderValidationResult
    {
        public bool IsValid { get; set; }
        public IReadOnlyList<string> MissingHeaders { get; set; } = Array.Empty<string>();
        public IReadOnlyList<string> UnexpectedHeaders { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Minimal header validator. For CSV files it reads the first line and checks header names.
    /// For non-CSV files it returns IsValid=false (caller can decide how to proceed).
    /// Replace or extend with Excel parsing logic if you need to validate .xlsx/.xls files.
    /// </summary>
    public static class ExcelValidator
    {
        public static HeaderValidationResult ValidateHeaders(string filePath, IEnumerable<string> expectedHeaders)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (expectedHeaders == null) throw new ArgumentNullException(nameof(expectedHeaders));

            var expected = expectedHeaders.Select(h => (h ?? string.Empty).Trim()).Where(h => h.Length > 0).ToArray();
            if (!File.Exists(filePath))
                throw new HeaderValidationException($"File not found: {filePath}", expectedHeaders: expected, unexpectedHeaders: Array.Empty<string>());

            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext == ".csv")
            {
                using var sr = new StreamReader(filePath);
                var first = sr.ReadLine() ?? string.Empty;
                var actual = first.Split(',').Select(h => h.Trim()).ToArray();

                var missing = expected.Where(e => !actual.Contains(e, StringComparer.OrdinalIgnoreCase)).ToArray();
                var unexpected = actual.Where(a => !expected.Contains(a, StringComparer.OrdinalIgnoreCase)).ToArray();

                return new HeaderValidationResult
                {
                    IsValid = missing.Length == 0,
                    MissingHeaders = missing,
                    UnexpectedHeaders = unexpected
                };
            }

            // For non-CSV files: indicate not validated
            return new HeaderValidationResult
            {
                IsValid = false,
                MissingHeaders = expected,
                UnexpectedHeaders = Array.Empty<string>()
            };
        }
    }

    public class HeaderValidationException : Exception
    {
        public IReadOnlyList<string> MissingHeaders { get; }
        public IReadOnlyList<string> UnexpectedHeaders { get; }

        public HeaderValidationException(string message) : this(message, Array.Empty<string>(), Array.Empty<string>()) { }

        public HeaderValidationException(string message, IEnumerable<string>? expectedHeaders = null, IEnumerable<string>? unexpectedHeaders = null)
            : base(message)
        {
            MissingHeaders = (expectedHeaders ?? Array.Empty<string>()).ToArray();
            UnexpectedHeaders = (unexpectedHeaders ?? Array.Empty<string>()).ToArray();
        }

        public HeaderValidationException(string message, Exception inner, IEnumerable<string>? expectedHeaders = null, IEnumerable<string>? unexpectedHeaders = null)
            : base(message, inner)
        {
            MissingHeaders = (expectedHeaders ?? Array.Empty<string>()).ToArray();
            UnexpectedHeaders = (unexpectedHeaders ?? Array.Empty<string>()).ToArray();
        }
    }
}