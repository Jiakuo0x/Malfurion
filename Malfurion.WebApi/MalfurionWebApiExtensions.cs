namespace Malfurion.WebApi;
using Malfurion.WebApi.Services;
using Malfurion.WebApi.Middlewares;
public static class MalfurionWebApiExtensions
{
    public static IServiceCollection AddMalfurionWebApi(this IServiceCollection services)
    {
        services.AddScoped<ResponseService>();
        return services;
    }

    public static IApplicationBuilder UseMalfurionWebApi(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}