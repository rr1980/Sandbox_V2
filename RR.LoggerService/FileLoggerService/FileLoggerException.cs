using RR.LoggerService.Core;
using System;

namespace RR.LoggerService.FileLoggerService
{
    public class FileLoggerException : LoggerException
    {
        public FileLoggerException(string msg) : base(msg)
        {
        }

        public FileLoggerException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }
}