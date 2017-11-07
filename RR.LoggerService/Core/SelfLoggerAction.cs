using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RR.LoggerService.Core
{
    internal class SelfLoggerAction : ILoggerAction
    {
        private readonly ILoggerConfiguration _loggerConfiguration;
        private readonly LogLevel _selfLogLevel;

        public SelfLoggerAction(LogLevel selfLogLevel, ILoggerConfiguration loggerConfiguration)
        {
            _selfLogLevel = selfLogLevel;
            _loggerConfiguration = loggerConfiguration;
        }

        public async Task LogAsync<TState>(ILoggerMessage<TState> loggerMessage)
        {
            if (loggerMessage.LogLevel >= _selfLogLevel)
            {
                Debug.WriteLine(DateTime.Now + " " + loggerMessage.LogLevel + " : " + loggerMessage.CategoryName + " : " + loggerMessage.Formatter(loggerMessage.State, loggerMessage.Exception));
                await Task.FromResult(1);
            }
        }
    }
}