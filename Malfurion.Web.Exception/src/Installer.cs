namespace Malfurion.Web.Exception;

public static class Installer
{
    public static IServiceCollection AddMalfurionWebException(this IServiceCollection services)
    {
        services.AddMalfurionWebResponser();
        return services;
    }
    public static IApplicationBuilder UseMalfurionWebException(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}