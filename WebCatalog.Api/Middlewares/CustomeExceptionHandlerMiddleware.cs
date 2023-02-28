using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace WebCatalog.Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        string result = string.Empty;

        switch (exception)
        {
            case ArgumentException argumentException:
                code = HttpStatusCode.BadRequest;
                result = argumentException.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (result.IsNullOrEmpty())
        {
            return Task.CompletedTask;
        }

        return context.Response.WriteAsync(result);
        // return context.Response.WriteAsync(JsonConvert.SerializeObject(errors));
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