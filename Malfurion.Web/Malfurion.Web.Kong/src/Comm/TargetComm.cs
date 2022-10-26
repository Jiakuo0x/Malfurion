namespace Malfurion.Web.Kong.Comm;

internal class TargetComm : CommBase
{
    public TargetComm(string adminApiAddr) : base(adminApiAddr) { }
    public async Task AddTarget(Guid upstreamId, string targetAddr)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync($"{base.AdminApiAddr}/{Upstreams}/{upstreamId}/{Targets}", new
        {
            Target = targetAddr,
        });
        var result = await response.Content.ReadAsStringAsync();
    }
}