﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using Zopoesht.Infrastructure.DomainValidation.Models;
using Zopoesht.Infrastructure.Logs;
using Zopoesht.Data.Logs.Enums;

namespace Zopoesht.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILoggingService logger)
        {
            await logger.LogRequestAsync(httpContext.Request);

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                await logger.LogExceptionAsync(ex, LogType.ExceptionLog, httpContext.Request);

                var statusCode = ex is DomainErrorException ? HttpStatusCode.UnprocessableEntity : HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, statusCode, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object content)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            var responseText = JsonConvert.SerializeObject(content, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return context.Response.WriteAsync(responseText);
        }
    }
}