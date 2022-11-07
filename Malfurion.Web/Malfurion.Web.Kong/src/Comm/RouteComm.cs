namespace Malfurion.Web.Kong.Comm;

internal class RouteComm : CommBase
{
    public RouteComm(string adminApiAddr) : base(adminApiAddr) { }
    public async Task AddRoute(string serviceName, string[] paths, string routeName)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{base.AdminApiAddr}/{Services}/{serviceName}/{Routes}", new
            {
                Name = routeName,
                Paths = paths,
            });
        var result = await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> HadRoute(string serviceName, string routeName)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{base.AdminApiAddr}/{Services}/{serviceName}/{Routes}/{routeName}");

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }
}