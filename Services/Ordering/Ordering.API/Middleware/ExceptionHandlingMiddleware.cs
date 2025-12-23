using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Ordering.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse
            {
                Success = false,
                Message = exception.Message
            };

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = "Validation failed";
                    errorResponse.Errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "An error occurred while processing your request";
                    _logger.LogError(exception, "An unexpected error occurred");
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse, options);
            await response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}

