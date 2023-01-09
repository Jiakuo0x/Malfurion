using Malfurion.Web.MQTT.C2S2C;
using Malfurion.Web.MQTT.C2S2C.Request;

var clientB = new Client();
await clientB.ConnectAsync("124.70.74.219", 22003, "test-client", "test01", "1qaz@WSX");
var requestClientB = new RequestClient(clientB);

Func<string, string?> funcB = (string message) =>
{
    Console.WriteLine($"Received: {message}");
    var response = $"Response: {message}";
    Console.WriteLine($"Response: {response}");
    return response;
};
await requestClientB.SubscribeRequestAsync("test-request", funcB);

Console.ReadKey();