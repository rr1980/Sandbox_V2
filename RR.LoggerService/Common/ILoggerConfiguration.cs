using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace RR.LoggerService.Common
{
    public interface ILoggerConfiguration
    {
        ConcurrentDictionary<string, LogLevel> LogLevel { get; set; }
    }
}
