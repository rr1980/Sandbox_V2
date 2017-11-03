using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;

namespace RR.LoggerService.Core
{
    internal class SelfLoggerAction : ILoggerAction
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly LogLevel _selfLogLevel;

        public SelfLoggerAction(LogLevel selfLogLevel, ILoggerConfiguration loggerConfiguration)
        {
            _selfLogLevel = selfLogLevel;
            _loggerConfiguration = loggerConfiguration;
        }

        public void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel >= _selfLogLevel)
            {
                Debug.WriteLine(DateTime.Now + " " + logLevel + " : " + categoryName + " : " + formatter(state, exception));
            }
        }
    }
}