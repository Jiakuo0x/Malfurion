namespace Malfurion.WebApi.Middlewares;
using Microsoft.Extensions.Logging;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        ILogger<ExceptionMiddleware> logger,
        Response response)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            var dto = response.InternalServerError(ex.Message);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(dto));
        }
    }
}