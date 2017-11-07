﻿using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RR.LoggerService.DebugLoggerService
{
    public class DebugLoggerAction : ILoggerAction
    {
        private DebugLoggerConfiguration _loggerConfiguration;
        private readonly ILogger _selfLogger;
        private readonly string _name;

        public DebugLoggerAction(string name, DebugLoggerConfiguration loggerConfiguration, ILogger selfLogger)
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
                _selfLogger.LogTrace("DebugLoggerAction init finish for: '" + _name + "'");
            }
            catch (Exception ex)
            {
                throw new DebugLoggerException("DebugLoggerAction ctor failed for: " + name, ex);
            }
        }

        public async Task LogAsync<TState>(ILoggerMessage<TState> loggerMessage)
        {
            try
            {
                Debug.WriteLine(DateTime.Now + " " + loggerMessage.LogLevel + " : " + loggerMessage.CategoryName + " : " + loggerMessage.Formatter(loggerMessage.State, loggerMessage.Exception));
                _selfLogger.LogTrace("DebugLoggerAction Log run: '" + loggerMessage.CategoryName + "'");
                await Task.FromResult(1);
            }
            catch (Exception ex)
            {
                throw new DebugLoggerException("DebugLoggerAction Log failed for: " + loggerMessage.CategoryName, ex);
            }
        }
    }
}