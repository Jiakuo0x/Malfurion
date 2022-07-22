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
        if (context.Request.Headers.TryGetValue(Constants.HttpHeader.RequestId, out var requestIdHeader) && requestIdHeader.Count > 0)
        {
            context.TraceIdentifier = string.Join(";", requestIdHeader);
        }
        else
        {
            context.TraceIdentifier = Guid.NewGuid().ToString();
        }

        await _next(context);

        if(!context.Response.Headers.TryAdd(Constants.HttpHeader.RequestId, context.TraceIdentifier))
        {
            context.Response.Headers.Remove(Constants.HttpHeader.RequestId);
            context.Response.Headers.Add(Constants.HttpHeader.RequestId, context.TraceIdentifier);
        }
    }
}