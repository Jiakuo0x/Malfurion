namespace Malfurion.Web.Responser;

public static class Installer
{
    public static IServiceCollection AddMalfurionWebResponser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<Response>();
        return services;
    }
}