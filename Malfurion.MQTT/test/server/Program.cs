using Malfurion.Web.MQTT.C2S2C;
using Malfurion.Web.MQTT.C2S2C.Request;

var clientA = new Client();
await clientA.ConnectAsync("124.70.74.219", 22003, "test-server", "test01", "1qaz@WSX");
var requestClientA = new RequestClient(clientA);

await requestClientA.SubscribeResponseAsync("test-response");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (string message, string toClient) =>
{
    var result = await requestClientA.RequestAsync("test-request", "test-response", toClient, message);
    return result;
});

app.Run();
