using RR.LoggerService.Common;
using System.Diagnostics;

namespace RR.LoggerService.FileLoggerService
{
    public class FileLoggerAction : ILoggerAction
    {
        private FileLoggerConfiguration _loggerConfiguration;

        public FileLoggerAction(FileLoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        public void Log(string v)
        {
            Debug.WriteLine("FileLogger " + v);
        }
    }
}
