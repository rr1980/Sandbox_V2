using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RR.LoggerService.Common
{
    public interface ILoggerAction
    {
        Task LogAsync<TState>(ILoggerMessage<TState> loggerMessage);
    }
}