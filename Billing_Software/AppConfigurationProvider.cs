#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using Billing_Software;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Billing_Software.Config
{
    // Named AppConfigurationProvider to avoid collision with Microsoft.Extensions.Configuration.ConfigurationProvider
    public static class AppConfigurationProvider
    {
        private static IConfiguration? _configuration;
        private static ILogger? _logger;

        public static void Initialize(IConfiguration? configuration = null, ILoggerFactory? loggerFactory = null)
        {
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger("AppConfigurationProvider")
                      ?? AppLogger.LoggerFactory?.CreateLogger("AppConfigurationProvider");
        }

        public static bool TryGetConnectionString(string name, out string? connectionString)
        {
            connectionString = _configuration?.GetConnectionString(name)
                             ?? System.Configuration.ConfigurationManager.ConnectionStrings[name]?.ConnectionString
                             ?? System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            var ok = !string.IsNullOrWhiteSpace(connectionString);
            if (!ok) _logger?.LogWarning("Connection string '{Name}' not found or empty.", name);
            else _logger?.LogDebug("Connection string '{Name}' loaded (len={Len}).", name, connectionString.Length);
            return ok;
        }

        public static bool ValidateConnectionStringSyntax(string name)
        {
            if (!TryGetConnectionString(name, out var cs)) return false;

            try
            {
                var builder = new DbConnectionStringBuilder { ConnectionString = cs! };
                var keys = builder.Keys.Cast<object>().Select(k => k.ToString()).ToArray();
                _logger?.LogInformation("Connection string '{Name}' parsed OK. Keys: {Keys}", name, string.Join(", ", keys));
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Connection string '{Name}' is invalid: {Message}", name, ex.Message);
                return false;
            }
        }

        public static bool BindAndValidate<T>(string sectionName, out T settings) where T : class, new()
        {
            settings = new T();
            var section = _configuration?.GetSection(sectionName);
            if (section != null && section.Exists())
            {
                TryBindSectionTo(settings, section);
                _logger?.LogDebug("Bound configuration section '{Section}' to {Type}.", sectionName, typeof(T).Name);
            }
            else
            {
                _logger?.LogDebug("Configuration section '{Section}' does not exist in IConfiguration.", sectionName);
            }

            var ctx = new ValidationContext(settings, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(settings, ctx, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var r in results)
                    _logger?.LogError("Validation error in '{Section}': {Error}", sectionName, r.ErrorMessage);
                return false;
            }

            _logger?.LogInformation("Configuration section '{Section}' bound and validated successfully.", sectionName);
            return true;
        }

        // Lightweight binder to avoid depending on Microsoft.Extensions.Configuration.Binder package.
        private static void TryBindSectionTo<T>(T target, IConfigurationSection section) where T : class
        {
            if (target == null || section == null) return;
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.CanWrite);
            foreach (var p in props)
            {
                try
                {
                    var val = section[p.Name];
                    if (val == null) continue;
                    object converted = null;
                    var pt = p.PropertyType;
                    if (pt == typeof(string)) converted = val;
                    else if (pt.IsEnum) converted = Enum.Parse(pt, val);
                    else converted = Convert.ChangeType(val, pt);
                    p.SetValue(target, converted);
                }
                catch
                {
                    // ignore conversion errors
                }
            }
        }



        public static string RuntimeValidateAndLog<T>(string sectionName, string connName) where T : class, new()
        {
            var diagnostics = new List<string>();

            if (ValidateConnectionStringSyntax(connName))
                diagnostics.Add($"Connection '{connName}' OK.");
            else
                diagnostics.Add($"Connection '{connName}' MISSING/INVALID.");

            if (BindAndValidate<T>(sectionName, out _))
                diagnostics.Add($"{sectionName} OK.");
            else
                diagnostics.Add($"{sectionName} MISSING/INVALID.");

            var diag = string.Join(" ", diagnostics);
            _logger?.LogInformation("Runtime configuration validation summary: {Summary}", diag);
            return diag;
        }
    }
}