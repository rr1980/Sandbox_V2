using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;

namespace RR.LoggerService.Core
{
    internal class LoggerMessage<TState> : ILoggerMessage<TState>
    {
        public string CategoryName { get; private set; }
        public string MethodName { get; private set; }
        public LogLevel LogLevel { get; private set; }
        public EventId EventId { get; private set; }
        public TState State { get; private set; }
        public Exception Exception { get; private set; }
        public Func<TState, Exception, string> Formatter { get; private set; }

        public LoggerMessage(string categoryName, string methodName, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            CategoryName = categoryName;
            MethodName = methodName;
            LogLevel = logLevel;
            EventId = eventId;
            State = state;
            Exception = exception;
            Formatter = formatter;
        }
    }
}