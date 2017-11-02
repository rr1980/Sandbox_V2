using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.LoggerService.Core;
using System;
using System.Linq;

namespace RR.LoggerService.DebugLoggerService
{
    public static class DebugLoggerExtension
    {
        public static IServiceCollection AddDebugLogger(this IServiceCollection services, DebugLoggerConfiguration loggerConfiguration)
        {
            #region throwExceptions

            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException("loggerConfiguration");
            }

            if (loggerConfiguration.LogLevel == null || !loggerConfiguration.LogLevel.Any())
            {
                throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
            }

            #endregion throwExceptions

            services.AddLogging(loggingBuilder =>
            {
                //ClearProviders()
                loggingBuilder.AddProvider(new LoggerProvider(new DebugLoggerAction(loggerConfiguration), loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);
            });

            return services;
        }
    }
}