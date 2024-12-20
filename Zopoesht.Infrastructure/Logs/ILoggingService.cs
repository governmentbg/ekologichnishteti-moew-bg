using Microsoft.AspNetCore.Http;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Infrastructure.Logs
{
    public interface ILoggingService : IScopedService
    {
        Task LogRequestAsync(HttpRequest request);

        Task LogExceptionAsync(Exception ex, LogType type, HttpRequest request = null);

        //Task LogMessageAsync(string message, HttpRequest request = null);

        //Task LogConsumerException(string message);
    }
}