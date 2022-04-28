namespace Malfurion.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
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
            var responseBody = new
            {
                Message = ex.Message,
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseBody));
        }
    }
}