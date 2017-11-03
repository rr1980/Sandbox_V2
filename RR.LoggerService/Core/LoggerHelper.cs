using Microsoft.Extensions.Logging;
using RR.LoggerService.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RR.LoggerService.Core
{
    internal static class LoggerHelper
    {
        internal static LogLevel GetFilterLogLvl(string categoryName, ILoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration.LogLevels.TryGetValue(categoryName, out var l))
            {
                return l;
            }
            else
            {
                var strA = categoryName.Split(".");
                for (int i = strA.Length; i > 0; i--)
                {

                    var v = String.Join(".", strA.Take(i));
                    if (loggerConfiguration.LogLevels.TryGetValue(v, out var ll))
                    {
                        return ll;
                    }
                }

            }

            return loggerConfiguration.MinLevel;
        }

        internal static ILoggerAction CreateInstance_ILoggerAction<T>(string name, ILoggerConfiguration loggerConfiguration, ILogger selfLogger) where T : class
        {
            return Activator.CreateInstance(typeof(T), new object[] { name, loggerConfiguration, selfLogger }) as ILoggerAction;
        }

        internal static async void FireAndForget(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                // log errors
            }
        }
    }


}