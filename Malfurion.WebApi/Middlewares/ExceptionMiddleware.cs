namespace Malfurion.WebApi.Middlewares;
using Microsoft.Extensions.Logging;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var responseBody = new Models.Response
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseBody));
            
            _logger.LogError(ex, ex.Message);
        }
    }
}