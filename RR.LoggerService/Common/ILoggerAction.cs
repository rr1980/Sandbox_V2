using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RR.LoggerService.Common
{
    public interface ILoggerAction
    {
        void Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}