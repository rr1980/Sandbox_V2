using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RR.LoggerService.Core
{
    internal class LoggerProvider<T> : ILoggerProvider where T : class, ILoggerAction 
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        private readonly ILogger _selfLogger;
        private readonly string _name;

        public LoggerProvider(string name, ILoggerConfiguration loggerConfiguration)
        {
            try
            {
                #region throwExceptions

                if (loggerConfiguration == null)
                {
                    throw new ArgumentNullException("loggerConfiguration");
                }

                //if (loggerConfiguration.LogLevels == null || !loggerConfiguration.LogLevels.Any())
                //{
                //    throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
                //}

                #endregion throwExceptions

                _name = string.IsNullOrEmpty(name) == true ? "SelfLoggerAction<" + typeof(T).Name + ">" : name;
                _loggerConfiguration = loggerConfiguration;

                _selfLogger = new Logger(name, _loggerConfiguration.SelfLogLevel, _loggerConfiguration, new SelfLoggerAction(_loggerConfiguration.SelfLogLevel, _loggerConfiguration));
                _selfLogger.LogDebug("LoggerProvider init finish");
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
                var __logLevel = LoggerHelper.GetFilterLogLvl(categoryName, _loggerConfiguration);
                _selfLogger.LogDebug("LoggerProvider CreateLogger with: '" + __logLevel + "' for '" + categoryName + "'");
                return _loggers.GetOrAdd(categoryName, cName => new Logger(cName, __logLevel, _loggerConfiguration, LoggerHelper.CreateInstance_ILoggerAction<T>(cName, _loggerConfiguration, _selfLogger), _selfLogger));
            }
            catch (Exception ex)
            {
                throw new LoggerException("LoggerProvider CreateLogger faild!", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}