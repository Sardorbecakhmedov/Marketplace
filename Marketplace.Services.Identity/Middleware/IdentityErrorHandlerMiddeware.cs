namespace Marketplace.Services.Identity.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class IdentityErrorHandlerMiddleware
{
    private readonly ILogger<IdentityErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public IdentityErrorHandlerMiddleware(RequestDelegate next, ILogger<IdentityErrorHandlerMiddleware> logger)
    {
         _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal IDENTITY server error!");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                Error = e.Message
            });
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IdentityErrorHandlerMiddleware>();
    }
}