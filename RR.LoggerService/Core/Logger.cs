using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

                var mn = _getMethodName(new StackTrace(), exception);
                var lm = new LoggerMessage<TState>(_categoryName, mn, logLevel, eventId, state, exception, formatter);

                _selfLogger?.LogTrace("Logger Log run '" + _categoryName + "' (fire and forget)");
                await _loggerAction.LogAsync<TState>(lm);
            }
            catch (Exception ex)
            {
                throw new LoggerException("Logger Log faild!", ex);
            }
        }

        private string _getMethodName(StackTrace stackTrace, Exception exception)
        {
            if(exception != null)
            {
                return _getLastExceptionTarget(exception).ToString();
            }
            else
            {
                return _getLastStackTraceTarget(stackTrace)?.ToString();
            }
        }

        private MethodBase _getLastStackTraceTarget(StackTrace stackTrace)
        {
            var m = stackTrace.GetFrames().FirstOrDefault(f=>f.GetMethod().DeclaringType.FullName == _categoryName);
            if (m != null)
            {
                return m.GetMethod();
            }
            else
            {
                return null;
            }
        }

        private MethodBase _getLastExceptionTarget(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return _getLastExceptionTarget(exception.InnerException);
            }
            else
            {
                return exception.TargetSite;
            }
        }
    }
}