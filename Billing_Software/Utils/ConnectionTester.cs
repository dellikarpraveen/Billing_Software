using System;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Billing_Software.Config;

namespace Billing_Software.Utils
{
    public static class ConnectionTester
    {
        /// <summary>
        /// Tests connection string presence, syntax, and attempts to open a SQL connection.
        /// Prints results to Console and returns true if the connection opened successfully.
        /// </summary>
        public static bool TestConnection(string connectionName)
        {
            try
            {
                Console.WriteLine($"Testing connection '{connectionName}'...");

                if (!AppConfigurationProvider.TryGetConnectionString(connectionName, out var cs))
                {
                    Console.WriteLine($"Connection string '{connectionName}' not found.");
                    return false;
                }

                Console.WriteLine($"Found connection string (masked): {MaskConnectionString(cs ?? string.Empty)}");

                if (!AppConfigurationProvider.ValidateConnectionStringSyntax(connectionName))
                {
                    Console.WriteLine("Connection string syntax is invalid.");
                    return false;
                }

                Console.WriteLine("Connection string syntax OK. Attempting to open SQL connection...");

                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    Console.WriteLine($"Connection opened successfully to server: {conn.DataSource}, database: {conn.Database}");
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }

        private static string MaskConnectionString(string cs)
        {
            if (string.IsNullOrEmpty(cs)) return string.Empty;
            // simple masking: hide password if present
            try
            {
                var builder = new DbConnectionStringBuilder { ConnectionString = cs };
                if (builder.ContainsKey("Password")) builder["Password"] = "****";
                if (builder.ContainsKey("Pwd")) builder["Pwd"] = "****";
                return builder.ConnectionString;
            }
            catch
            {
                // fallback: show only first 80 chars
                return cs.Length <= 80 ? cs : cs.Substring(0, 80) + "...";
            }
        }
    }
}
