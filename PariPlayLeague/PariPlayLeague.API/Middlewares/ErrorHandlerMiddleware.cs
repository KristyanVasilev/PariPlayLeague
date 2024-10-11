using PariPlayLeague.Application.ResultPattern.ErrorResults;
using PariPlayLeague.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PariPlayLeague.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var response = exception switch
            {
                NotFoundException => new ErrorResult(exception.Message, StatusCodes.Status404NotFound),
                BadRequestException => new ErrorResult(exception.Message, StatusCodes.Status400BadRequest),
                UnauthorizedException => new ErrorResult(exception.Message, StatusCodes.Status401Unauthorized),
                ValidationException => new ErrorResult(exception.Message, StatusCodes.Status400BadRequest),
                _ => new ErrorResult(exception.Message, StatusCodes.Status500InternalServerError)
            };
            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
