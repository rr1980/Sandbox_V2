using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService.DebugLoggerService;
using Tests.Extensions;

namespace Tests.RR.Logger
{
    [TestClass]
    public class LoggerDebugTests
    {
        private ILoggerFactory loggerFactory;

        [TestInitialize(), TestCategory("LoggerDebug")]
        public void LoggerDebugTests_Init()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            AssertH.DoesNotThrow(() => serviceCollection.AddLogging());
            Assert.IsNotNull(serviceCollection);

            DebugLoggerConfiguration _loggerConfiguration = new DebugLoggerConfiguration();
            _loggerConfiguration.LogLevel.GetOrAdd("LoggerDebugTest", LogLevel.Trace);
            serviceCollection.AddDebugLogger(_loggerConfiguration);

            ServiceProvider serviceProvider = null;
            AssertH.DoesNotThrow(() => serviceProvider = serviceCollection.BuildServiceProvider());
            Assert.IsNotNull(serviceProvider);

            AssertH.DoesNotThrow(() => loggerFactory = serviceProvider.GetService<ILoggerFactory>());
            Assert.IsNotNull(loggerFactory);
        }

        [TestMethod(), TestCategory("LoggerDebug")]
        public void GetDebugLogger()
        {
            var _logger = loggerFactory.CreateLogger<LoggerDebugTests>();
            AssertH.DoesNotThrow(() => _logger.LogInformation("GetLogger pass...!"));
        }
    }
}