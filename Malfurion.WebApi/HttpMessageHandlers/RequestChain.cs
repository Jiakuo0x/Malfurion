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

        var responseChainHeader = response.Headers.GetValues(HttpHeader.RequestChain);

        var tempHeader = _httpContextAccessor.HttpContext.Response.Headers[HttpHeader.RequestChain].ToList();
        tempHeader.AddRange(responseChainHeader);
        _httpContextAccessor.HttpContext.Response.Headers[HttpHeader.RequestChain] = tempHeader.ToArray();

        return response;
    }
}