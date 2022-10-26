namespace Malfurion.Web.RequestLog;

public static class Installer
{
    public static IServiceCollection AddRequestLog(this IServiceCollection services)
    {
        services.AddMalfurionWebResponser();
        return services;
    }
    public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}