namespace Malfurion.WebApi.HttpMessageHandlers;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Malfurion.WebApi.Constants;
public class RequestChain : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RequestChain(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        var tempHeader = _httpContextAccessor.HttpContext.Response.Headers[HttpHeader.RequestChain].ToList();
        if (response.Headers.TryGetValues(HttpHeader.RequestChain, out var responseChainHeader))
        {
            tempHeader.AddRange(responseChainHeader);
        }
        else
        {
            tempHeader.Add(request.RequestUri?.ToString());
        }
        _httpContextAccessor.HttpContext.Response.Headers[HttpHeader.RequestChain] = tempHeader.ToArray();

        return response;
    }
}