using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;

namespace RR.LoggerService.DebugLoggerService
{
    public class DebugLoggerAction : ILoggerAction
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

            #endregion throwExceptions

            _name = name;
            _loggerConfiguration = loggerConfiguration;
            _selfLogger = selfLogger;
            _selfLogger.LogTrace("DebugLoggerAction init finish for: '" + _name + "'");
        }

        public void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine(DateTime.Now + " " + logLevel + " : " + categoryName + " : " + formatter(state, exception));
            _selfLogger.LogTrace("DebugLoggerAction Log run: '" + categoryName + "'");
        }
    }
}