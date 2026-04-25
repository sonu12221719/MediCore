using System;
using System.Text.Json;
using BillingLibrary.Exceptions;

namespace Billing.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next   = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BillingServiceException ex)
        {
            _logger.LogWarning(ex, "Billing exception: {Message}", ex.Message);
            await WriteErrorResponse(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponse(context, 500, "An unexpected error occurred.");
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = statusCode;

        var response = JsonSerializer.Serialize(new
        {
            statusCode,
            message,
            timestamp = DateTime.UtcNow
        });

        await context.Response.WriteAsync(response);
    }
}
