using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;

namespace RR.LoggerService.Core
{
    internal class Logger : ILogger
    {
        private readonly string _categoryName;
        private readonly ILoggerAction _loggerAction;
        private readonly LogLevel _filter;

        public Logger(string categoryName, LogLevel filter, ILoggerAction loggerAction)
        {
            try
            {
                #region throwExceptions

                if (string.IsNullOrEmpty(categoryName))
                {
                    throw new ArgumentNullException("categoryName");
                }

                if (loggerAction == null)
                {
                    throw new ArgumentNullException("loggerAction");
                }

                #endregion throwExceptions

                _categoryName = categoryName;
                _filter = filter;
                _loggerAction = loggerAction;
            }
            catch (Exception ex)
            {
                throw new LoggerException("Logger ctor faild!", ex);
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _filter;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(!IsEnabled(logLevel))
            {
                return;
            }

            try
            {
                _loggerAction.Log(_categoryName);
            }
            catch (Exception ex)
            {
                throw new LoggerException("Logger Log faild!", ex);
            }
        }
    }
}