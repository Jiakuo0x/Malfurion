namespace Malfurion.Web.MQTT.C2S2C.Request;

public class RequestClient
{
    private Client _client;
    public RequestClient(Client client)
    {
        _client = client;
    }

    private Dictionary<Guid, MessageInfo> _requestCache = new Dictionary<Guid, MessageInfo>();
    public async Task<MessageInfo> RequestAsync<TRequest>(string requestTopic, string responseTopic, string toClientId, TRequest message, int timeout = 3)
    {
        var request = new MessageInfo
        {
            RequestFromClientId = _client.ClientId,
            RequestToClientId = toClientId,
            RequestMessage = JsonConvert.SerializeObject(message, Formatting.Indented),
            RequestTopic = requestTopic,
            ResponseTopic = responseTopic,
        };

        lock (_requestCache)
        {
            _requestCache.Add(request.RequestId, request);
        }

        try
        {
            var sendStr = JsonConvert.SerializeObject(request);
            Console.WriteLine($"MQTT Send request message: {sendStr}");
            await _client.PublishAsync(requestTopic, JsonConvert.SerializeObject(request, Formatting.Indented));

            for (var i = 0; i < timeout; i++)
            {
                await Task.Delay(1000);
                if (request.IsResponded)
                    break;
            }
        }
        finally
        {
            lock (_requestCache)
            {
                _requestCache.Remove(request.RequestId);
            }
        }

        return request;
    }
    public async Task SubscribeResponseAsync(string topic)
    {
        _client.AddSubscribeEvent(message =>
        {
            Console.WriteLine($"MQTT Received response message: {message}");

            MessageInfo request;
            try
            {
                request = JsonConvert.DeserializeObject<MessageInfo>(message)!;
            }
            catch
            {
                Console.WriteLine($"MQTT message format error.");
                return;
            }

            if (request.RequestFromClientId != _client.ClientId)
            {
                Console.WriteLine($"MQTT message is not for this client.");
                return;
            }

            lock (_requestCache)
            {
                if (_requestCache.TryGetValue(request.RequestId, out var cacheRequest))
                {
                    cacheRequest.ResponseMessage = request.ResponseMessage;
                    cacheRequest.IsResponded = request.IsResponded;
                }
            }
        });
        await _client.SubscribeAsync(topic);
    }
    public async Task SubscribeRequestAsync<TRequest, TResponse>(string requestTopic, Func<TRequest, TResponse> func)
    {
        _client.AddSubscribeEvent(async message =>
        {
            Console.WriteLine($"MQTT Received request message: {message}");
            MessageInfo request;
            try
            {
                request = JsonConvert.DeserializeObject<MessageInfo>(message)!;
            }
            catch
            {
                Console.WriteLine($"MQTT message format error.");
                return;
            }

            if (request.RequestToClientId != _client.ClientId)
            {
                Console.WriteLine($"MQTT message is not for this client.");
                return;
            }

            var requestMessage = JsonConvert.DeserializeObject<TRequest>(request.RequestMessage);
            var response = func(requestMessage!);
            request.IsResponded = true;

            if (response is null)
                return;

            request.ResponseMessage = JsonConvert.SerializeObject(response);

            await _client.PublishAsync(request.ResponseTopic, JsonConvert.SerializeObject(request, Formatting.Indented));
        });
        await _client.SubscribeAsync(requestTopic);
    }
}