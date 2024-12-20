using Microsoft.AspNetCore.Http;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Infrastructure.DomainValidation.Models;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Data.Logs;
using Microsoft.AspNetCore.Http.Extensions;
using Zopoesht.Infrastructure.Logs.Extensions;

namespace Zopoesht.Infrastructure.Logs
{
    public class LoggingService : ILoggingService
    {
        private readonly Interfaces.Contexts.IAppLogContext context;

        public LoggingService(IAppLogContext context)
        {
            this.context = context;
        }

        public async Task LogExceptionAsync(Exception exception, LogType type, HttpRequest request = null)
        {
            string exceptionMessage;
            if (exception is DomainErrorException customException)
            {
                exceptionMessage = "Message:" + " " + string.Join(", ", customException.ErrorMessages.Select(x => x.DomainErrorCode)) + " " + $"\nStackTrace: {exception.StackTrace}";
            }
            else
            {
                exceptionMessage = $"Message: {exception.Message} \nStackTrace: {exception.StackTrace}";
            }

            await this.LogAsync(type, exceptionMessage, request, true);
        }

        public async Task LogRequestAsync(HttpRequest request)
        {
            await this.LogAsync(LogType.TraceLog, null, request);
        }

        //public async Task LogMessageAsync(string message, HttpRequest request = null)
        //{
        //    await this.LogAsync(LogType.InformationMessageLog, message, request);
        //}

        //public async Task LogConsumerException(string message)
        //{
        //    await this.LogAsync(LogType.RabbitMQ, message);
        //}

        private async Task LogAsync(LogType type, string message, HttpRequest request = null, bool ignoreRequestBody = false)
        {
            var log = new Log
            {
                Type = type,
                LogDate = DateTime.UtcNow,
                IP = request?.HttpContext.Connection.RemoteIpAddress.ToString(),
                Verb = request?.Method,
                Url = request?.GetDisplayUrl(),
                UserAgent = request?.Headers["User-Agent"].ToString(),
                Message = message,
                UserId = request?.GetUserId(),
                RequestBody = await request.GetRequestBody(ignoreRequestBody)
            };

            this.context.Set<Log>().Add(log);
            await this.context.SaveChangesAsync();
        }
    }
}
