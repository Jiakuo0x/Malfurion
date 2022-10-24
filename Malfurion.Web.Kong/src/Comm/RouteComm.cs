namespace Malfurion.Web.Kong.Comm;

internal class RouteComm : CommBase
{
    public RouteComm(string adminApiAddr) : base(adminApiAddr) { }
    public async Task AddRoute(string serviceName, string[] paths)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{base.AdminApiAddr}/{Services}/{serviceName}/{Routes}", new
            {
                Paths = paths,
            });
        var result = await response.Content.ReadAsStringAsync();
    }
}