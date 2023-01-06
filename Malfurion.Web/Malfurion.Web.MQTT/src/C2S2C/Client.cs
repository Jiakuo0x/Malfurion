namespace Malfurion.Web.MQTT.C2S2C;

public class Client
{
    private readonly IMqttClient _mqttClient;
    private readonly string _clientId;

    public Client(string clientId)
    {
        _mqttClient = new MqttFactory().CreateMqttClient();
        _clientId = clientId;
    }

    public async Task ConnectAsync(string host, int port, string username, string password)
    {
        if (_mqttClient.IsConnected)
            await _mqttClient.DisconnectAsync();

        await _mqttClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId(_clientId)
            .WithCredentials(username, password)
            .WithTcpServer(host, port)
            .Build());
    }

    public async Task Reconnect() => await _mqttClient.ReconnectAsync();

    public async Task Disconnect() => await _mqttClient.DisconnectAsync();

    public async Task PublishAsync(string topic, string message)
    {
        await _mqttClient.PublishStringAsync(topic, message);
    }

    public async Task SubscribeAsync(string topic, Action<string> action, Action<Exception>? exceptionHandler = null)
    {
        _mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                action(message);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
                return Task.CompletedTask;
            }
        };
        await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(topic)
            .Build());
    }

    public async Task UnsubscribeAsync(string topic) => await _mqttClient.UnsubscribeAsync(topic);
}