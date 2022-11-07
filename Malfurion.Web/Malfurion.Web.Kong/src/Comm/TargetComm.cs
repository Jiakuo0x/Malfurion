namespace Malfurion.Web.Kong.Comm;
using Dtos;
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

    public async Task<TargetsReadDto?> GetTargets(string upstreamName)
    {
        HttpClient client = new HttpClient();
        var responseMessage = await client.GetAsync($"{base.AdminApiAddr}/{Upstreams}/{upstreamName}/{Targets}/all/");

        if (responseMessage.IsSuccessStatusCode)
            return await responseMessage.Content.ReadFromJsonAsync<TargetsReadDto>();
        else
            return null;
    }
}