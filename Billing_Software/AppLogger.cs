#nullable enable

using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;

namespace Billing_Software
{
    /// <summary>
    /// Small application-wide logger helper.
    /// Initializes a Serilog-backed <see cref="ILoggerFactory"/> that other classes use via <see cref="LoggerFactory"/>.
    /// </summary>
    public static class AppLogger
    {
        private static readonly object _sync = new object();
        private static bool _initialized;

        /// <summary>
        /// Public logger factory consumers should use. May be null until the static initializer runs.
        /// </summary>
        public static ILoggerFactory? LoggerFactory { get; private set; }

        /// <summary>
        /// Serilog logger instance (for sinks / static logging).
        /// </summary>
        public static Serilog.ILogger? SerilogLogger { get; private set; }

        static AppLogger()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the logging stack. Safe to call multiple times.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            lock (_sync)
            {
                if (_initialized) return;

                // Basic Serilog configuration. Keep minimal to avoid requiring extra sink packages here.
                var cfg = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext();

                // You can enable file/Seq/sinks here if packages and configuration are available.
                SerilogLogger = cfg.CreateLogger();

                // Create Microsoft.Extensions.Logging factory backed by Serilog
                LoggerFactory = new SerilogLoggerFactory(SerilogLogger, dispose: true);

                _initialized = true;
            }
        }

        /// <summary>
        /// Convenience accessor to get a typed logger.
        /// </summary>
        public static ILogger<T> CreateLogger<T>() => (LoggerFactory ?? Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance).CreateLogger<T>();

        /// <summary>
        /// Dispose logging resources. Call on app shutdown if desired.
        /// </summary>
        public static void Shutdown()
        {
            lock (_sync)
            {
                try
                {
                    LoggerFactory?.Dispose();
                    Serilog.Log.CloseAndFlush();
                }
                catch { /* best-effort */ }
                finally
                {
                    LoggerFactory = null;
                    SerilogLogger = null;
                    _initialized = false;
                }
            }
        }
    }
}