using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService;
using RR.LoggerService.Core;
using RR.LoggerService.DebugLoggerService;
using RR.LoggerService.FileLoggerService;
using System;
using Tests.Extensions;

namespace Tests.RR.LoggerService
{
    [TestClass]
    public class LoggerFileExtensionTests
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;

        [TestInitialize(), TestCategory("LoggerFile")]
        public void LoggerFileExtensionTests_Init()
        {
            serviceCollection = new ServiceCollection();
            AssertH.DoesNotThrow(() => serviceCollection.AddLogging());
            Assert.IsNotNull(serviceCollection);

            AssertH.DoesNotThrow(() => serviceProvider = serviceCollection.BuildServiceProvider());
            Assert.IsNotNull(serviceProvider);

            ILoggerFactory _loggerFactory = null;
            AssertH.DoesNotThrow(() => _loggerFactory = serviceProvider.GetService<ILoggerFactory>());
            Assert.IsNotNull(_loggerFactory);
        }

        [TestMethod(), TestCategory("LoggerFile")]
        public void AddFileLogger_Throw()
        {
            Assert.ThrowsException<ArgumentNullException>(()=> serviceCollection.AddFileLogger(null));

            FileLoggerConfiguration _loggerConfiguration = new FileLoggerConfiguration()
            {
                LogLevel = null

            };
            Assert.ThrowsException<ArgumentException>(() => serviceCollection.AddFileLogger(_loggerConfiguration));
        }

        [TestMethod(), TestCategory("LoggerFile")]
        public void AddFileLogger()
        {
            FileLoggerConfiguration _loggerConfiguration = new FileLoggerConfiguration();
            _loggerConfiguration.LogLevel.GetOrAdd("LoggerFileTest", LogLevel.Trace);

            AssertH.DoesNotThrow(() => serviceCollection.AddFileLogger(_loggerConfiguration));
        }
    }
}
