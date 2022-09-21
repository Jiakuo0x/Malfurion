namespace Malfurion.WebApi.Middlewares;
using Microsoft.Extensions.Logging;

public class HttpCodeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpCodeMiddleware> _logger;
    private readonly Services.ResponseService _response;
    public HttpCodeMiddleware(
        RequestDelegate next,
        ILogger<HttpCodeMiddleware> logger,
        Services.ResponseService response)
    {
        _next = next;
        _logger = logger;
        _response = response;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = _response.Result.HttpCode;
        }
    }
}