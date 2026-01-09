using FCG.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace FCG.Api.Middlewares
{
    public class ErroMiddleware
    {
        private readonly RequestDelegate _next;

        public ErroMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedException => HttpStatusCode.Unauthorized,
                ForbiddenException => HttpStatusCode.Forbidden,
                ConflictException => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };

            object response = ex switch
            {
                ValidationException ve => new
                {
                    status = (int)statusCode,
                    message = ve.Message,
                    errors = ve.Errors
                },
                NotFoundException nf => new
                {
                    status = (int)statusCode,
                    message = nf.Message
                },  
                _ => new
                {
                    status = (int)statusCode,
                    message = "Ocorreu um erro inesperado.",
                    detail = ex.Message
                }
            };

            context.Response.StatusCode = (int)statusCode;
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}