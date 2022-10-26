namespace Malfurion.Web.Middlewares;
public class ApiRequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiRequestLogMiddleware> _logger;

    public ApiRequestLogMiddleware(
        RequestDelegate next,
        ILogger<ApiRequestLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestReader = new StreamReader(context.Request.Body);
        var requestBody = await requestReader.ReadToEndAsync();
        requestBody = requestBody.Replace("\n", "");
        context.Request.Body.Position = 0;

        var ms = new MemoryStream();
        var originBody = context.Response.Body;
        context.Response.Body = ms;

        await _next(context);
        ms.Position = 0;
        var responseReader = new StreamReader(ms);
        var responseBody = await responseReader.ReadToEndAsync();
        ms.Position = 0;
        await ms.CopyToAsync(originBody);
        context.Response.Body = originBody;

        if (context.Request.Method != "OPTIONS")
        {
            _logger.LogInformation($"Request: [{context.Request.Method}]:{context.Request.Path}\n{requestBody}\nResponse: [{context.Response.StatusCode}]\n{responseBody}");
        }
    }
}