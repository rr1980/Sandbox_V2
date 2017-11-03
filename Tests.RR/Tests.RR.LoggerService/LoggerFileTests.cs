using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService;
using RR.LoggerService.Core;
using RR.LoggerService.FileLoggerService;
using System.Collections.Generic;
using Tests.Extensions;

namespace Tests.RR.Logger
{
    [TestClass]
    public class LoggerFileTests
    {
        private ILoggerFactory loggerFactory;

        [TestInitialize(), TestCategory("LoggerFile")]
        public void LoggerFileTests_Init()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            AssertH.DoesNotThrow(() => serviceCollection.AddLogging());
            Assert.IsNotNull(serviceCollection);

            FileLoggerConfiguration _loggerConfiguration = new FileLoggerConfiguration();
            _loggerConfiguration.LogLevels.GetOrAdd("LoggerFileTest",  LogLevel.Trace );
            //_loggerConfiguration.MinLevel = LogLevel.Warning;

            AssertH.DoesNotThrow(() => serviceCollection.AddRRLogger<FileLoggerConfiguration, FileLoggerAction>("FileLogger", _loggerConfiguration));

            ServiceProvider serviceProvider = null;
            AssertH.DoesNotThrow(() => serviceProvider = serviceCollection.BuildServiceProvider());
            Assert.IsNotNull(serviceProvider);

            AssertH.DoesNotThrow(() => loggerFactory = serviceProvider.GetService<ILoggerFactory>());
            Assert.IsNotNull(loggerFactory);
        }

        [TestMethod(), TestCategory("LoggerFile")]
        public void GetFileLogger()
        {
            var _logger = loggerFactory.CreateLogger<LoggerFileTests>();
            AssertH.DoesNotThrow(() => _logger.LogInformation("GetLogger pass...!"));
        }
    }
}