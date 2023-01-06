namespace Malfurion.Web.MQTT.C2S2C;

public class RequestClient
{
    private Client _client;
    public RequestClient(Client client)
    {
        _client = client;
    }

    private Dictionary<Guid, RequestInfo> _requestCache = new Dictionary<Guid, RequestInfo>();
    public async Task<RequestInfo> RequestAsync<TRequest>(string requestTopic, TRequest message, int timeout = 3)
    {
        var request = new RequestInfo
        {
            Payload = JsonConvert.SerializeObject(message)
        };

        lock (_requestCache)
        {
            _requestCache.Add(request.RequestId, request);
        }

        try
        {
            await _client.PublishAsync(requestTopic, JsonConvert.SerializeObject(request));

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
        await _client.SubscribeAsync(topic, message =>
        {
            var request = JsonConvert.DeserializeObject<RequestInfo>(message);
            if (request is null)
                return;

            if (_requestCache.TryGetValue(request.RequestId, out var cachedRequest))
            {
                cachedRequest.Response = request.Response;
                cachedRequest.IsResponded = true;
            }
            else
                return;
        });
    }
    public async Task SubscribeRequestAsync<TRequest, TResponse>(string requestTopic, string responseTopic, Func<TRequest, TResponse> func)
    {
        await _client.SubscribeAsync(requestTopic, async message =>
        {
            var request = JsonConvert.DeserializeObject<RequestInfo>(message);
            if (request is null)
                return;

            var requestMessage = JsonConvert.DeserializeObject<TRequest>(request.Payload);
            
            var response = func(requestMessage!);

            if(response is null)
                return;

            request.Response = JsonConvert.SerializeObject(response);

            await _client.PublishAsync(responseTopic, JsonConvert.SerializeObject(request));
        });
    }
}