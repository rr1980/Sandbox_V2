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
        private readonly Func<string, LogLevel, bool> _filter;

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

                _filter = (category, logLevel) =>
                {
                    var lls = _loggerConfiguration.LogLevel.FirstOrDefault(x => category.ToLower().Trim() == x.Key.ToLower().Trim());
                    if (lls.Key != null)
                    {
                        return logLevel >= lls.Value;
                    }

                    var splts = category.Split(".");
                    var given = _loggerConfiguration.LogLevel.ToDictionary(x => x.Key, y => y.Value);
                    var index = 0;

                    while (given.Count > 1 && index < splts.Length)
                    {
                        var s = splts[index];
                        var tmp = given.Where(g => g.Key.Split(".").Length > index);
                        given = tmp.Where(g => g.Key.Split(".")[index] == s).ToDictionary(x => x.Key, y => y.Value);

                        index++;
                    }

                    var t = 0;

                    return logLevel >= given.FirstOrDefault().Value;
                };
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
                return _loggers.GetOrAdd(categoryName, cName => new Logger(cName, _filter, _loggerAction));
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