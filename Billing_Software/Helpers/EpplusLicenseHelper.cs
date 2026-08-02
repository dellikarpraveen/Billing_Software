using System;
using System.Reflection;

namespace Billing_Software.Helpers
{
    internal static class EpplusLicenseHelper
    {
        /// <summary>
        /// Set EPPlus license to NonCommercial using the available API.
        /// Tries the newer ExcelPackage.License property first, falls back to LicenseContext if needed.
        /// Uses reflection so this works across EPPlus 5/6/7/8 variations.
        /// </summary>
        public static void SetNonCommercial()
        {
            try
            {
                var t = Type.GetType("OfficeOpenXml.ExcelPackage, EPPlus") ?? Type.GetType("OfficeOpenXml.ExcelPackage");
                if (t == null) return;

                // Try property named 'License' (newer API)
                var prop = t.GetProperty("License", BindingFlags.Public | BindingFlags.Static);
                if (prop != null && prop.CanWrite)
                {
                    var enumType = prop.PropertyType;
                    var val = Enum.Parse(enumType, "NonCommercial");
                    prop.SetValue(null, val);
                    return;
                }

                // Fallback to 'LicenseContext'
                var prop2 = t.GetProperty("LicenseContext", BindingFlags.Public | BindingFlags.Static);
                if (prop2 != null && prop2.CanWrite)
                {
                    var enumType = prop2.PropertyType;
                    var val = Enum.Parse(enumType, "NonCommercial");
                    prop2.SetValue(null, val);
                    return;
                }
            }
            catch
            {
                // best-effort; do not throw if reflection fails
            }
        }
    }
}
