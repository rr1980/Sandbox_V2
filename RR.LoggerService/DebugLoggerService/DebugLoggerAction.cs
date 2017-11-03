using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RR.LoggerService.DebugLoggerService
{
    internal class DebugLoggerAction : ILoggerAction
    {
        private DebugLoggerConfiguration _loggerConfiguration;
        private readonly ILogger _selfLogger;
        private readonly string _name;

        public DebugLoggerAction(string name, DebugLoggerConfiguration loggerConfiguration, ILogger selfLogger)
        {
            #region throwExceptions

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException("loggerConfiguration");
            }

            if (selfLogger == null)
            {
                throw new ArgumentNullException("selfLogger");
            }

            if (loggerConfiguration.LogLevels == null || !loggerConfiguration.LogLevels.Any())
            {
                throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
            }

            #endregion throwExceptions

            _name = name;
            _loggerConfiguration = loggerConfiguration;
            _selfLogger = selfLogger;
            _selfLogger.LogTrace("DebugLoggerAction init finish for: '" + _name + "'");
        }

        public void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine(DateTime.Now + " " + logLevel + " : " + categoryName + " : " + formatter(state, exception));
            _selfLogger.LogTrace("DebugLoggerAction Log run: '"+ categoryName + "'");
        }
    }
}