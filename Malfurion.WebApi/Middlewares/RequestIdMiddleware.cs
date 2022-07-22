namespace Malfurion.WebApi.Middlewares;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;
    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        if(context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader))
        {
            if(requestIdHeader.Count > 0)
            {
                context.TraceIdentifier = string.Join(";", requestIdHeader);
            }
        }
        else
        {
            context.TraceIdentifier = Guid.NewGuid().ToString();
        }
        
        await _next(context);

        context.Response.Headers.TryAdd("X-Request-Id", context.TraceIdentifier);
    }
}