using Malfurion.Web.MQTT.C2S2C;

var client = new Client(Guid.NewGuid().ToString());
await client.ConnectAsync("124.70.74.219", 22003, "test01", "1qaz@WSX");
var requestClient = new RequestClient(client);
await requestClient.SubscribeResponseAsync("response-topic");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{message}", async (string message) =>
{
    Console.WriteLine($"Sent: {message}");
    var result = await requestClient.RequestAsync("request-topic", 0);
    
    return Results.Ok(result);
});

app.Run();
