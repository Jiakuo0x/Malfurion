using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Malfurion.Web.Kong;

public class RegisterService : BackgroundService
{
     private readonly IOptions<Configurations.KongConf> _options;
    public RegisterService(IOptions<Configurations.KongConf> options)
    {
        _options = options;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var serviceComm = new Comm.ServiceComm(_options.Value.AdminApiAddress);
        var isHad = await serviceComm.HadService(_options.Value.ServiceName);
        if (!isHad)
            await serviceComm.AddService(_options.Value.ServiceName);

        var routeComm = new Comm.RouteComm(_options.Value.AdminApiAddress);
        await routeComm.AddRoute(_options.Value.ServiceName, new string[] { $"/{_options.Value.ServiceName}" });

        var upstreamComm = new Comm.UpstreamComm(_options.Value.AdminApiAddress);
        var upstream = await upstreamComm.SearchUpstream(_options.Value.ServiceName);
        if (upstream is null)
            upstream = await upstreamComm.AddUpstream(_options.Value.ServiceName);

        var targetComm = new Comm.TargetComm(_options.Value.AdminApiAddress);
        await targetComm.AddTarget(upstream.Id, _options.Value.ServiceAddress);
    }
}