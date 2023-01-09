namespace Malfurion.Web.MQTT.C2S2C;

public class Client
{
    private readonly IMqttClient _mqttClient;

    public Client()
    {
        _mqttClient = new MqttFactory().CreateMqttClient();
    }

    public string ClientId => _mqttClient.Options.ClientId;
    public bool IsConnected => _mqttClient.IsConnected;

    public async Task ConnectAsync(string host, int port, string clientId, string username, string password)
    {
        if (_mqttClient.IsConnected)
            await _mqttClient.DisconnectAsync();

        await _mqttClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId(clientId)
            .WithCredentials(username, password)
            .WithTcpServer(host, port)
            .Build());
    }

    public async Task Reconnect() => await _mqttClient.ReconnectAsync();

    public async Task Disconnect() => await _mqttClient.DisconnectAsync();

    public async Task PublishAsync(string topic, string message) => await _mqttClient.PublishStringAsync(topic, message);

    public async Task SubscribeAsync(string topic) => await _mqttClient.SubscribeAsync(topic);

    public async Task UnsubscribeAsync(string topic) => await _mqttClient.UnsubscribeAsync(topic);

    public void AddSubscribeEvent(Action<string> messageAction)
    {
        _mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            messageAction(message);
            return Task.CompletedTask;
        };
    }
}