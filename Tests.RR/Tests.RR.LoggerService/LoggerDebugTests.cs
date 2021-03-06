﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.LoggerService;
using RR.LoggerService.DebugLoggerService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //_loggerConfiguration.LogLevel.GetOrAdd("Tests.RR.Logger.LoggerDebugTests", new List<LogLevel>() { LogLevel.Warning });
            //_loggerConfiguration.LogLevel.GetOrAdd("Tests.RR.Logger",  LogLevel.Warning );
            _loggerConfiguration.LogLevels.GetOrAdd("aaa.RR.Logger", LogLevel.Warning);
            _loggerConfiguration.LogLevels.GetOrAdd("bbb.RR", LogLevel.Warning);
            _loggerConfiguration.LogLevels.GetOrAdd("bbb", LogLevel.Warning);
            _loggerConfiguration.LogLevels.GetOrAdd("int", LogLevel.Trace);
            _loggerConfiguration.LogLevels.GetOrAdd("Tests.RR", LogLevel.Trace);
            _loggerConfiguration.MinLevel = LogLevel.Trace;
            _loggerConfiguration.SelfLogLevel = LogLevel.None;
            serviceCollection.AddRRLogger<DebugLoggerConfiguration, DebugLoggerAction>("DebugLogger", _loggerConfiguration, true);

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

            var _logger2 = loggerFactory.CreateLogger<int>();

            _logger2.LogTrace(1, new Exception("NÖÖÖ!"), "ERR:", "Rene");

            AssertH.DoesNotThrow(() => _logger2.LogInformation("_logger2 pass...!"));

        }
    }
}