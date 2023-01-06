using Malfurion.Web.MQTT.C2S2C;

var client = new Client(Guid.NewGuid().ToString());
await client.ConnectAsync("124.70.74.219", 22003, "test01", "1qaz@WSX");
var requestClient = new RequestClient(client);

Func<int, string?> func = (int message) =>
{
    Console.WriteLine($"Received: {message}");
    var response = $"Response: {message}";
    Console.WriteLine($"Response: {response}");
    return null;
};
await requestClient.SubscribeRequestAsync("request-topic", "response-topic", func);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{message}", (string message) => Results.Ok(message));

app.Run();
