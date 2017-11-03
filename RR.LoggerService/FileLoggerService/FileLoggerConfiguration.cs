using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System.Collections.Concurrent;

namespace RR.LoggerService.FileLoggerService
{
    public class FileLoggerConfiguration : ILoggerConfiguration
    {
        public ConcurrentDictionary<string, LogLevel> LogLevels { get; set; } = new ConcurrentDictionary<string, Microsoft.Extensions.Logging.LogLevel>();
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;
    }
}