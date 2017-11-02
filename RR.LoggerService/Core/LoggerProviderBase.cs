//using Microsoft.Extensions.Logging;
//using RR.LoggerService.Common;
//using System.Collections.Concurrent;

//namespace RR.LoggerService.Core
//{
//    abstract class LoggerProviderBase : Common.ILoggerProviderT
//    {
//        protected readonly ConcurrentDictionary<string, ILoggerT> _loggers = new ConcurrentDictionary<string, ILoggerT>();

//        public virtual ILogger CreateLogger(string categoryName)
//        {
//            return null;
//        }

//        public void Dispose()
//        {

//        }
//    }
//}
