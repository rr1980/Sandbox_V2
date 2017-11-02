using RR.LoggerService.Core;
using System;

namespace RR.LoggerService.DebugLoggerService
{
    public class DebugLoggerException : LoggerException
    {
        public DebugLoggerException(string msg) : base(msg)
        {
        }

        public DebugLoggerException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }
}