using FCG.Application.Interfaces.Services;
using FCG.Domain.Models;
using System.Diagnostics;

namespace FCG.Api.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public LogMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
                stopwatch.Stop();

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var log = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Message = $"[{context.Request.Method}] {context.Request.Path}",
                    Level = "Info",
                    Source = "Middleware",
                    RequestBody = requestBody,
                    ResponseBody = responseText,
                    StatusCode = context.Response.StatusCode,
                    DurationMs = stopwatch.ElapsedMilliseconds
                };

                await _logService.SaveAsync(log);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                var errorLog = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Message = $"Erro na requisição: {ex.Message}",
                    Level = "Error",
                    Source = "Middleware",
                    RequestBody = requestBody,
                    StatusCode = 500,
                    DurationMs = stopwatch.ElapsedMilliseconds
                };

                await _logService.SaveAsync(errorLog);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Erro interno.");
            }
        }
    }
}