using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using RR.LoggerService.Core;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RR.LoggerService.Core
{

    class LoggerProvider : ILoggerProvider
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        private readonly ILoggerAction _loggerAction;

        public LoggerProvider(ILoggerAction loggerAction, ILoggerConfiguration loggerConfiguration)
        {
            #region throwExceptions

            if (loggerAction == null)
            {
                throw new ArgumentNullException("loggerAction");
            }

            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException("loggerConfiguration");
            }

            if (loggerConfiguration.LogLevel == null || !loggerConfiguration.LogLevel.Any())
            {
                throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
            }
            #endregion

            _loggerAction = loggerAction;
            _loggerConfiguration = loggerConfiguration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, cName => new Logger(cName, _loggerAction));
        }

        public void Dispose()
        {
        }
    }
}
