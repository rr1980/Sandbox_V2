﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.LoggerService;
using RR.LoggerService.DebugLoggerService;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            var lf = serviceProvider.GetService<ILoggerFactory>();
            var logger = lf.CreateLogger<Program>();

            try
            {
                logger.LogDebug("Try start Task");
                var task = Task.Run((Action)app.Run);
                logger.LogInformation("Task started");

                task.Wait();
            }
            catch(Exception ex)
            {
                throw new Exception("Fail!", ex);
            }
            Console.WriteLine("End");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //var configuration = new ConfigurationBuilder()
            //                        .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            //                        .AddJsonFile("appsettings.json", false)
            //                        .Build();

            services.AddOptions();

            //var ccc = configuration.GetSection("AppSettings");

            //services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            //var options = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            //services.AddLogger(options.Value.LoggerConfiguration);
            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration();

            _loggerConfiguration.LogLevels.GetOrAdd("Main", LogLevel.Warning);
            _loggerConfiguration.MinLevel = LogLevel.Information;
            _loggerConfiguration.SelfLogLevel = LogLevel.Warning;
            services.AddRRLogger<DebugLoggerConfiguration, DebugLoggerAction>("DebugLogger", _loggerConfiguration, true);


            services.AddTransient<IExampleService, ExampleService>();

            services.AddTransient<App>();
        }
    }
}
