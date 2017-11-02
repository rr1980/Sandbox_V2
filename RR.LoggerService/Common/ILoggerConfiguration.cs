using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace RR.LoggerService.Common
{
    internal interface ILoggerConfiguration
    {
        ConcurrentDictionary<string, LogLevel> LogLevel { get; set; }
    }
}