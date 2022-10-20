namespace Malfurion.Web.Kong;

public class RegisterManager
{
    private readonly IOptions<Configurations.KongConf> _options;

    public RegisterManager(IOptions<Configurations.KongConf> options)
    {
        _options = options;
    }

    public async Task Register()
    {
        await this.RegisterUpstream();
        await this.RegisterService();
    }

    #region Register Service
    private async Task RegisterService()
    {
        var isHad = await HadService();
        if (!isHad)
            await AddService();
        await AddRoute();
    }

    private async Task AddService()
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{_options.Value.AdminApiAddress}/{Services}", new
            {
                Name = _options.Value.ServiceName,
                Host = _options.Value.ServiceName,
                Port = 80,
            });
    }
    private async Task<bool> HadService()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(
            $"{_options.Value.AdminApiAddress}/{Services}/{_options.Value.ServiceName}");

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }
    private async Task AddRoute()
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{_options.Value.AdminApiAddress}/{Services}/{_options.Value.ServiceName}/{Routes}", new
            {
                Paths = new string[] { $"/{_options.Value.ServiceName}" },
            });
        var result = await response.Content.ReadAsStringAsync();
    }
    #endregion

    #region Register upstream
    private async Task<Models.Upstream> RegisterUpstream()
    {
        var upstream = await SearchUpstream();
        if (upstream is null)
            upstream = await AddUpstream();
        await AddTarget(upstream.Id);
        return upstream;
    }
    private async Task<Models.Upstream> AddUpstream()
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{_options.Value.AdminApiAddress}/{Upstreams}", new
            {
                Name = _options.Value.ServiceName,
            });
        var result = await response.Content.ReadFromJsonAsync<Models.Upstream>();

        if (result is null)
            throw new SystemException("Failed to create Upstream.");

        return result;
    }
    private async Task<Models.Upstream?> SearchUpstream()
    {
        HttpClient client = new HttpClient();
        var test = $"{_options.Value.AdminApiAddress}/{Upstreams}/{_options.Value.ServiceName}";
        var responseMessage = await client.GetAsync(
            $"{_options.Value.AdminApiAddress}/{Upstreams}/{_options.Value.ServiceName}");

        if (responseMessage.IsSuccessStatusCode)
            return await responseMessage.Content.ReadFromJsonAsync<Models.Upstream>();
        else
            return null;
    }

    private async Task AddTarget(Guid upstreamId)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync($"{_options.Value.AdminApiAddress}/{Upstreams}/{upstreamId}/{Targets}", new
        {
            Target = _options.Value.ServiceAddress,
        });
        var result = await response.Content.ReadAsStringAsync();
    }
    #endregion
}