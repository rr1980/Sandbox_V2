using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.LoggerService.Core;
using System;
using System.Linq;

namespace RR.LoggerService.DebugLoggerService
{
    public static class DebugLoggerExtension
    {
        public static IServiceCollection AddDebugLogger(this IServiceCollection services, DebugLoggerConfiguration loggerConfiguration, bool cleanAllProviders = false)
        {
            try
            {
                #region throwExceptions

                if (loggerConfiguration == null)
                {
                    throw new ArgumentNullException("loggerConfiguration");
                }

                if (loggerConfiguration.LogLevels == null || !loggerConfiguration.LogLevels.Any())
                {
                    throw new ArgumentException("Collection loggerConfiguration.LogLevel is null or count = zero!", "loggerConfiguration.LogLevel");
                }

                #endregion throwExceptions

                var tmp = services.FirstOrDefault(s => s.ServiceType == typeof(ILoggingBuilder));

                services.AddLogging(loggingBuilder =>
                {
                    if (cleanAllProviders)
                    {
                        loggingBuilder.ClearProviders();
                    }
                    loggingBuilder.AddProvider(new LoggerProvider<DebugLoggerAction>(loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);
                });

                return services;
            }
            catch (Exception ex)
            {
                throw new DebugLoggerException("AddDebugLogger faild!", ex);
            }
        }
    }
}