using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace RR.LoggerService.FileLoggerService
{
    internal class FileLoggerAction : ILoggerAction
    {
        private FileLoggerConfiguration _loggerConfiguration;
        private readonly ILogger _selfLogger;

        public FileLoggerAction(FileLoggerConfiguration loggerConfiguration, ILogger selfLogger)
        {
            #region throwExceptions

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

            _loggerConfiguration = loggerConfiguration;
            _selfLogger = selfLogger;
        }

        public void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine(DateTime.Now + " " + logLevel + " : " + categoryName + " : " + formatter(state, exception));
        }
    }
}