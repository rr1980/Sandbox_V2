using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService.DebugLoggerService;
using System;
using System.Collections.Generic;
using Tests.Extensions;

namespace Tests.RR.LoggerService
{
    [TestClass]
    public class LoggerDebugExtensionTests
    {
        private ServiceCollection serviceCollection;
        private ServiceProvider serviceProvider;

        [TestInitialize(), TestCategory("LoggerDebug")]
        public void LoggerDebugExtensionTests_Init()
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

        [TestMethod(), TestCategory("LoggerDebug")]
        public void AddDebugLogger_Throw()
        {
            Assert.ThrowsException<DebugLoggerException>(() => serviceCollection.AddDebugLogger(null));

            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration()
            {
                LogLevel = null
            };
            Assert.ThrowsException<DebugLoggerException>(() => serviceCollection.AddDebugLogger(_loggerConfiguration));
        }

        [TestMethod(), TestCategory("LoggerDebug")]
        public void AddDebugLogger()
        {
            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration();
            _loggerConfiguration.LogLevel.GetOrAdd("LoggerDebugTest",  LogLevel.Trace );

            AssertH.DoesNotThrow(() => serviceCollection.AddDebugLogger(_loggerConfiguration));
        }
    }
}