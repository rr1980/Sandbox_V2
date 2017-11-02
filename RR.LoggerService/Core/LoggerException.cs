using System;
using System.Collections.Generic;
using System.Text;

namespace RR.LoggerService.Core
{
    public class LoggerException : Exception
    {
        public LoggerException(string msg) : base(msg)
        {
        }

        public LoggerException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }
}
