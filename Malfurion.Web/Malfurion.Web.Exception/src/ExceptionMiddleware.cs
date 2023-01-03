namespace Malfurion.Web.Exception;
internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ILogger<ExceptionMiddleware> logger, Response response)
    {
        try
        {
            await _next(httpContext);
        }
        catch(BadRequest ex)
        {
            logger.LogError(ex, ex.Message);

            string responseStr = JsonConvert.SerializeObject(response.BadRequest(ex.Message));
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            await httpContext.Response.WriteAsync(responseStr);
        }
        catch(Unauthorized ex)
        {
            logger.LogError(ex, ex.Message);

            string responseStr = JsonConvert.SerializeObject(response.Unauthorized(ex.Message));
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            await httpContext.Response.WriteAsync(responseStr);
        }
        catch(InternalServerError ex)
        {
            logger.LogError(ex, ex.Message);

            string responseStr = JsonConvert.SerializeObject(response.InternalServerError(ex.Message));
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            await httpContext.Response.WriteAsync(responseStr);
        }
        catch(System.Exception ex)
        {
            logger.LogError(ex, ex.Message);
            
            string responseStr = JsonConvert.SerializeObject(response.InternalServerError("Internal server error."));
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            await httpContext.Response.WriteAsync(responseStr);
        }
    }
}