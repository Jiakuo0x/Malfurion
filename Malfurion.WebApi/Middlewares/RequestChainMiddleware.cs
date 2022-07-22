namespace Malfurion.WebApi.Middlewares;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

public class RequestChainMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestChainMiddleware> _logger;
    public RequestChainMiddleware(
        RequestDelegate next,
        ILogger<RequestChainMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(Constants.HttpHeader.RequestChain, out StringValues requestChainHeader))
        {
            var tempHeader = context.Response.Headers[Constants.HttpHeader.RequestChain].ToList();
            tempHeader.Add($"{context.Request.Host}{context.Request.Path}");
            context.Response.Headers[Constants.HttpHeader.RequestChain] = tempHeader.ToArray();
        }
        else
        {
            context.Response.Headers.Add(Constants.HttpHeader.RequestChain, $"{context.Request.Host}{context.Request.Path}");
        }

        await _next(context);
    }
}