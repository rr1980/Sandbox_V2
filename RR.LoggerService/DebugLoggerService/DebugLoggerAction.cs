using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Linq;

namespace RR.LoggerService.DebugLoggerService
{
    internal class DebugLoggerAction : ILoggerAction
    {
        private DebugLoggerConfiguration _loggerConfiguration;

        public DebugLoggerAction(DebugLoggerConfiguration loggerConfiguration)
        {
            #region throwExceptions

            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException("loggerConfiguration");
            }

            if (loggerConfiguration.LogLevel == null || !loggerConfiguration.LogLevel.Any())
            {
                throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
            }

            #endregion throwExceptions

            _loggerConfiguration = loggerConfiguration;
        }

        public void Log(string v)
        {
            Debug.WriteLine("DebugLogger " + v);
        }
    }
}