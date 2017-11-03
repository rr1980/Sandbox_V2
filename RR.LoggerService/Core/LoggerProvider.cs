using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RR.LoggerService.Core
{
    internal class LoggerProvider : ILoggerProvider
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        private readonly ILoggerAction _loggerAction;

        public LoggerProvider(ILoggerAction loggerAction, ILoggerConfiguration loggerConfiguration)
        {
            try
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

                #endregion throwExceptions

                _loggerAction = loggerAction;
                _loggerConfiguration = loggerConfiguration;
            }
            catch (Exception ex)
            {
                throw new LoggerException("LoggerProvider ctor faild!", ex);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            try
            {
                return _loggers.GetOrAdd(categoryName, cName => new Logger(cName, _getFilterLogLvl(categoryName), _loggerAction));
            }
            catch (Exception ex)
            {
                throw new LoggerException("LoggerProvider CreateLogger faild!", ex);
            }
        }

        public void Dispose()
        {
        }

        private LogLevel _getFilterLogLvl(string categoryName)
        {
            if (_loggerConfiguration.LogLevel.TryGetValue(categoryName, out var l))
            {
                return l;
            }
            else
            {
                var strA = categoryName.Split(".");
                for (int i = strA.Length; i > 0; i--)
                {

                    var v = String.Join(".", strA.Take(i));
                    if (_loggerConfiguration.LogLevel.TryGetValue(v, out var ll))
                    {
                        return ll;
                    }
                }

            }

            if (_loggerConfiguration.LogLevel.TryGetValue("Default", out var lll))
            {
                return l;
            }

            return LogLevel.Trace;
        }
    }
}