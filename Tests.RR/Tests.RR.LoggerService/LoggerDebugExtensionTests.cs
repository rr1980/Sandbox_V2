using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService;
using RR.LoggerService.Core;
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
            Assert.ThrowsException<LoggerException>(() => serviceCollection.AddRRLogger<DebugLoggerConfiguration, DebugLoggerAction>("DebugLogger", null));

            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration()
            {
                LogLevels = null
            };
            Assert.ThrowsException<LoggerException>(() => serviceCollection.AddRRLogger<DebugLoggerConfiguration, DebugLoggerAction>("DebugLogger", _loggerConfiguration));
        }

        [TestMethod(), TestCategory("LoggerDebug")]
        public void AddDebugLogger()
        {
            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration();
            _loggerConfiguration.LogLevels.GetOrAdd("LoggerDebugTest",  LogLevel.Trace );

            AssertH.DoesNotThrow(() => serviceCollection.AddRRLogger<DebugLoggerConfiguration, DebugLoggerAction>("DebugLogger", _loggerConfiguration));
        }
    }
}