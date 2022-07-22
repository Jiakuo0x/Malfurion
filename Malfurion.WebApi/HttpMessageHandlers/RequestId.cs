namespace Malfurion.WebApi.HttpMessageHandlers;
using Malfurion.WebApi.Constants;
public class RequestId : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RequestId(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add(HttpHeader.RequestId, _httpContextAccessor.HttpContext.TraceIdentifier);

        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}