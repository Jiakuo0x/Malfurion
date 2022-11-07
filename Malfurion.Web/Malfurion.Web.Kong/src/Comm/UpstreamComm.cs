namespace Malfurion.Web.Kong.Comm;
using Dtos;

internal class UpstreamComm : CommBase
{
    public UpstreamComm(string adminApiAddr) : base(adminApiAddr) { }
    public async Task<UpstreamReadDto> AddUpstream(string upstreamName)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{base.AdminApiAddr}/{Upstreams}", new
            {
                Name = upstreamName,
            });
        var result = await response.Content.ReadFromJsonAsync<UpstreamReadDto>();

        if (result is null)
            throw new SystemException("Failed to create Upstream.");

        return result;
    }

    public async Task<UpstreamReadDto?> SearchUpstream(string upstreamName)
    {
        HttpClient client = new HttpClient();
        var responseMessage = await client.GetAsync($"{base.AdminApiAddr}/{Upstreams}/{upstreamName}");

        if (responseMessage.IsSuccessStatusCode)
            return await responseMessage.Content.ReadFromJsonAsync<UpstreamReadDto>();
        else
            return null;
    }
}