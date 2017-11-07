using Microsoft.Extensions.Logging;
using System;

namespace RR.LoggerService.Common
{
    public interface ILoggerMessage<TState>
    {
         string CategoryName { get; }
         LogLevel LogLevel { get; }
         EventId EventId { get; }
         TState State { get; }
         Exception Exception { get; }
         Func<TState, Exception, string> Formatter { get; }
    }
}