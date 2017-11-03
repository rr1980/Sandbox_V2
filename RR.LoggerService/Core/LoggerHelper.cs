using RR.LoggerService.Common;
using System;

namespace RR.LoggerService.Core
{
    internal static class LoggerHelper
    {
        public static ILoggerAction CreateInstance_ILoggerAction<T>(ILoggerConfiguration loggerConfiguration) where T : class
        {
            return Activator.CreateInstance(typeof(T), new[] { loggerConfiguration }) as ILoggerAction;
        }

    }
}