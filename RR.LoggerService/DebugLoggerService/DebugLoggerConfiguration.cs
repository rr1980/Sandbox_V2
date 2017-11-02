using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System.Collections.Concurrent;

namespace RR.LoggerService.DebugLoggerService
{
    public class DebugLoggerConfiguration : ILoggerConfiguration
    {
        public ConcurrentDictionary<string, LogLevel> LogLevel { get; set; } = new ConcurrentDictionary<string, Microsoft.Extensions.Logging.LogLevel>();
    }
}