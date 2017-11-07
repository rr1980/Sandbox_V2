using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RR.LoggerService.Core
{
    internal class Logger : ILogger
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly string _categoryName;
        private readonly ILoggerAction _loggerAction;
        private readonly LogLevel _filter;
        private readonly ILogger _selfLogger;

        public Logger(string categoryName, LogLevel filter, ILoggerConfiguration loggerConfiguration, ILoggerAction loggerAction, ILogger selfLogger = null)
        {
            try
            {
                #region throwExceptions

                if (string.IsNullOrEmpty(categoryName))
                {
                    throw new ArgumentNullException("categoryName");
                }

                if (loggerConfiguration == null)
                {
                    throw new ArgumentNullException("loggerConfiguration");
                }

                if (loggerAction == null)
                {
                    throw new ArgumentNullException("loggerAction");
                }

                #endregion throwExceptions
                
                _categoryName = categoryName;
                _filter = filter;
                _loggerConfiguration = loggerConfiguration;
                _loggerAction = loggerAction;
                _selfLogger = selfLogger;
                _selfLogger?.LogTrace("Logger init with: '" + filter + "' for '" + categoryName + "'");
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
            if(_selfLogger == null)
            {
                return logLevel >= _filter;
            }
            var result = logLevel >= _loggerConfiguration.MinLevel && logLevel >= _filter;
            _selfLogger?.LogTrace("Logger IsEnabled: '"+ result + "' with '" + logLevel + "' for '" + _categoryName + "'");
            return result;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }


                var lm = new LoggerMessage<TState>(_categoryName, logLevel, eventId, state, exception, formatter);

                _selfLogger?.LogTrace("Logger Log run '" + _categoryName + "' (fire and forget)");
                await _loggerAction.LogAsync<TState>(lm);
                //Task.Run(() => _loggerAction.Log(_categoryName, logLevel, eventId, state, exception, formatter));
            }
            catch (Exception ex)
            {
                throw new LoggerException("Logger Log faild!", ex);
            }
        }
    }

    internal class LoggerMessage<TState> : ILoggerMessage<TState>
    {
        public string CategoryName { get; private set; }
        public LogLevel LogLevel { get; private set; }
        public EventId EventId { get; private set; }
        public TState State { get; private set; }
        public Exception Exception { get; private set; }
        public Func<TState, Exception, string> Formatter { get; private set; }

        public LoggerMessage(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            CategoryName = categoryName;
            LogLevel = logLevel;
            EventId = eventId;
            State = state;
            Exception = exception;
            Formatter = formatter;
        }
    }
}