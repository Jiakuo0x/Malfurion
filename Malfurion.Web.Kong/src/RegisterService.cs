using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Malfurion.Web.Kong;

public class RegisterService : BackgroundService
{
    private readonly RegisterManager _registerManager;
    public RegisterService(RegisterManager registerManager)
    {
        _registerManager = registerManager;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _registerManager.Register();
    }
}