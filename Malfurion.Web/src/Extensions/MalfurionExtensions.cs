namespace Malfurion.Web.Extensions;

public static class MalfurionExtensions
{
    public static IServiceCollection AddMalfurionWeb(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<Services.Response>();
        return services;
    }

    public static IApplicationBuilder UseMalfurionWeb(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}