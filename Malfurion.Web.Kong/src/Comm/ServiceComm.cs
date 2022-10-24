namespace Malfurion.Web.Kong.Comm;

internal class ServiceComm : CommBase
{
    public ServiceComm(string adminApiAddr) : base(adminApiAddr) { }
    public async Task AddService(string serviceName, string? serviceHost = null, int? port = null)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(
            $"{base.AdminApiAddr}/{Services}", new
            {
                Name = serviceName,
                Host = serviceHost ?? serviceName,
                Port = port ?? 80,
            });
    }

    public async Task<bool> HadService(string serviceName)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{base.AdminApiAddr}/{Services}/{serviceName}");

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }
}