using System.Net;
using System.Text.Json;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Api.Middlewares;

/// <summary>
/// Middleware для обработки ошибок.
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly bool _isDevelopment;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
    {
        _next = next;
        _isDevelopment = environment.IsDevelopment();
    }

    public async Task Invoke(HttpContext context, IAppLogger<CustomExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, logger);
        }
    }

    /// <summary>
    /// Обработать ошибку.
    /// </summary>
    /// <param name="context">Контекст Http запроса.</param>
    /// <param name="exception">Ошибка.</param>
    /// <param name="logger">Логгер.</param>
    /// <returns>Обработанная ошибка.</returns>
    private Task HandleExceptionAsync(
        HttpContext context,
        Exception exception, 
        IAppLogger<CustomExceptionHandlerMiddleware> logger)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case WebCatalogValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case WebCatalogNotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = notFoundException.Message;
                break;
            case WebCatalogDublicateException dublicateException:
                code = HttpStatusCode.Conflict;
                result = dublicateException.Message;
                break;
            case WebCatalogEmptyBasketException emptyBasketException:
                code = HttpStatusCode.NotFound;
                result = emptyBasketException.Message;
                break;
            default:
                logger.LogError($"Unhandled exception: {exception.Message}");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (_isDevelopment)
        {
            result = JsonSerializer.Serialize(new {error = $"{exception}"});
        }

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        return builder;
    }
}