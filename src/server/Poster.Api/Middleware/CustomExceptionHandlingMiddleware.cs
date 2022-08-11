using System.Net;
using System.Text.Json;
using Poster.Application.Common.Exceptions;

namespace Poster.Api.Middleware;

public class CustomExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlingMiddleware(RequestDelegate next)
        => _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = string.Empty;
        var errorCode = exception switch
        {
            ArgumentException argumentException => HttpStatusCode.BadRequest,
            NotFoundException notFoundException => HttpStatusCode.BadRequest,
            AlreadyFollowingException alreadyFollowingException => HttpStatusCode.BadRequest,
            IsNotFollowingException isNotFollowingException => HttpStatusCode.BadRequest,
            AuthorException authorException => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) errorCode;
        
        if (error == string.Empty)
            error = JsonSerializer.Serialize(new { error = exception.Message });

        Console.WriteLine(123);
        
        await context.Response.WriteAsync(error);
    }
}

public static class CustomExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this
        IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlingMiddleware>();
    }
}