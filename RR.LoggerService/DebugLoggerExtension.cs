using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using RR.LoggerService.Core;
using System;
using System.Linq;

namespace RR.LoggerService
{
    public static class RRLoggerExtension
    {
        public static IServiceCollection AddRRLogger<TLoggerConfiguration, TLoggerAction>(this IServiceCollection services, string name, TLoggerConfiguration loggerConfiguration, bool cleanAllProviders = false) where TLoggerConfiguration : ILoggerConfiguration  where TLoggerAction : class, ILoggerAction
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

                services.AddLogging(loggingBuilder =>
                {
                    if (cleanAllProviders)
                    {
                        loggingBuilder.ClearProviders();
                    }
                    loggingBuilder.AddProvider(new LoggerProvider<TLoggerAction>(name, loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);
                });

                return services;
            }
            catch (Exception ex)
            {
                throw new LoggerException("AddDebugLogger faild!", ex);
            }
        }
    }
}