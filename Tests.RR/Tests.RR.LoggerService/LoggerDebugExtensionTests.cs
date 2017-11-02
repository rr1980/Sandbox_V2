using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService;
using RR.LoggerService.Core;
using RR.LoggerService.DebugLoggerService;
using System;
using Tests.Extensions;

namespace Tests.RR.LoggerService
{
    [TestClass]
    public class LoggerDebugExtensionTests
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;

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
            Assert.ThrowsException<ArgumentNullException>(()=> serviceCollection.AddDebugLogger(null));

            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration()
            {
                LogLevel = null

            };
            Assert.ThrowsException<ArgumentException>(() => serviceCollection.AddDebugLogger(_loggerConfiguration));
        }

        [TestMethod(), TestCategory("LoggerDebug")]
        public void AddDebugLogger()
        {
            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration();
            _loggerConfiguration.LogLevel.GetOrAdd("LoggerDebugTest", LogLevel.Trace);

            AssertH.DoesNotThrow(() => serviceCollection.AddDebugLogger(_loggerConfiguration));
        }
    }
}
