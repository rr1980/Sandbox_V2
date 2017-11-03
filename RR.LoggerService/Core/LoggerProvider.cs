using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RR.LoggerService.Core
{
    internal class LoggerProvider<T> : ILoggerProvider where T : class
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();

        public LoggerProvider(ILoggerConfiguration loggerConfiguration)
        {
            try
            {
                #region throwExceptions

                if (loggerConfiguration == null)
                {
                    throw new ArgumentNullException("loggerConfiguration");
                }

                if (loggerConfiguration.LogLevels == null || !loggerConfiguration.LogLevels.Any())
                {
                    throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
                }

                #endregion throwExceptions

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
                return _loggers.GetOrAdd(categoryName, cName => new Logger(cName, _getFilterLogLvl(categoryName), LoggerHelper.CreateInstance_ILoggerAction<T>(_loggerConfiguration)));
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
            if (_loggerConfiguration.LogLevels.TryGetValue(categoryName, out var l))
            {
                return l;
            }
            else
            {
                var strA = categoryName.Split(".");
                for (int i = strA.Length; i > 0; i--)
                {

                    var v = String.Join(".", strA.Take(i));
                    if (_loggerConfiguration.LogLevels.TryGetValue(v, out var ll))
                    {
                        return ll;
                    }
                }

            }

            //if (_loggerConfiguration.LogLevels.TryGetValue("Default", out var lll))
            //{
            //    return l;
            //}

            return _loggerConfiguration.MinLevel;
        }
    }
}