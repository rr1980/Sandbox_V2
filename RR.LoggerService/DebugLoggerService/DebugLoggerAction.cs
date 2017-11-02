using RR.LoggerService.Common;
using System.Diagnostics;

namespace RR.LoggerService.DebugLoggerService
{
    public class DebugLoggerAction : ILoggerAction
    {
        private DebugLoggerConfiguration _loggerConfiguration;

        public DebugLoggerAction(DebugLoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        public void Log(string v)
        {
            Debug.WriteLine("DebugLogger " + v);
        }
    }
}
