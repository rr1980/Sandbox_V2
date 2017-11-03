﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.LoggerService.Core;
using System;
using System.Linq;

namespace RR.LoggerService.FileLoggerService
{
    public static class FileLoggerExtension
    {
        public static IServiceCollection AddFileLogger(this IServiceCollection services, FileLoggerConfiguration loggerConfiguration, bool cleanAllProviders = false)
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
                    loggingBuilder.AddProvider(new LoggerProvider<FileLoggerAction>("FileLogger", loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);
                });

                return services;
            }
            catch (Exception ex)
            {
                throw new FileLoggerException("AddFileLogger faild!", ex);
            }
        }
    }
}