﻿using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;

namespace RR.LoggerService.FileLoggerService
{
    public class FileLoggerAction : ILoggerAction
    {
        private FileLoggerConfiguration _loggerConfiguration;
        private readonly ILogger _selfLogger;
        private readonly string _name;

        public FileLoggerAction(string name, FileLoggerConfiguration loggerConfiguration, ILogger selfLogger)
        {
            try
            {
                #region throwExceptions

                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException("name");
                }

                if (loggerConfiguration == null)
                {
                    throw new ArgumentNullException("loggerConfiguration");
                }

                if (selfLogger == null)
                {
                    throw new ArgumentNullException("selfLogger");
                }

                #endregion throwExceptions

                _name = name;
                _loggerConfiguration = loggerConfiguration;
                _selfLogger = selfLogger;
                _selfLogger.LogTrace("FileLoggerAction init finish for: '" + _name + "'");
            }
            catch (Exception ex)
            {
                throw new FileLoggerException("FileLoggerAction ctor failed for: " + name, ex);
            }
        }

        public void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                Debug.WriteLine(DateTime.Now + " " + logLevel + " : " + categoryName + " : " + formatter(state, exception));
                _selfLogger.LogTrace("FileLoggerAction Log run: '" + categoryName + "'");
            }
            catch (Exception ex)
            {
                throw new FileLoggerException("FileLoggerAction Log failed for: " + categoryName, ex);
            }
        }
    }
}