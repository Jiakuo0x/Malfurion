namespace Malfurion.WebApi.Middlewares;
using Microsoft.Extensions.Logging;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly Services.ResponseService _response;
    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        Services.ResponseService response)
    {
        _next = next;
        _logger = logger;
        _response = response;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var dto = _response.InternalServerError(ex.Message);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(dto));
        }
    }
}