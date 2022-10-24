namespace Malfurion.Web.Kong;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class Extentions
{
    public static IServiceCollection AddMalfurionWebKong(this IServiceCollection services)
    {
        services.AddHostedService<RegisterService>();
        return services;
    }
}