using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace RR.LoggerService.Common
{
    public interface ILoggerConfiguration
    {
        LogLevel MinLevel { get; set; }
        LogLevel SelfLogLevel { get; set; }
        ConcurrentDictionary<string, LogLevel> LogLevels { get; set; }
    }
}