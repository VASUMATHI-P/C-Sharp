using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ProductMicroServices.Middlewares
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly string logFilePath = "log.txt";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var log = $"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}\n";
                File.AppendAllText(logFilePath, log);

                await next(context);

                var responseLog = $"[{DateTime.Now}] Response: {context.Response.StatusCode}\n";
                File.AppendAllText(logFilePath, responseLog);
            }
            catch (Exception ex)
            {
                var errorLog = $"[{DateTime.Now}] Error: {ex.Message}\nStack Trace: {ex.StackTrace}\n";
                File.AppendAllText(logFilePath, errorLog);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
