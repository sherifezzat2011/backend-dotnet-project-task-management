using System.Text.Json;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.Exceptions;

namespace ProjectTaskManagement.API.Middleware;

public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            logger.LogWarning(ex, "Handled application exception");
            await WriteErrorAsync(context, ex.StatusCode, ex.Message, ex is ValidationException validationException ? validationException.Errors : null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteErrorAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    private static Task WriteErrorAsync(HttpContext context, int statusCode, string message, IReadOnlyList<ValidationError>? errors = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var payload = JsonSerializer.Serialize(new
        {
            success = false,
            message,
            errors
        });

        return context.Response.WriteAsync(payload);
    }
}
