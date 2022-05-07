namespace Malfurion.WebApi.Services.Instances;
using Models;

internal class ResponseService : IResponseService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ResponseService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Response Success()
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        return new Response { IsSuccess = true };
    }
    public Response Success(object data)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        return new() { IsSuccess = true, Payload = data };
    }
    public Response<T> Success<T>(T data)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        return new() { IsSuccess = true, Payload = data };
    }
    public Response Failure()
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return new Response { IsSuccess = false };
    }
    public Response Failure(string message)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return new Response { IsSuccess = false, ErrorMessage = message };
    }
    public Response Failure(string code, string message)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return new Response { IsSuccess = false, ErrorCode = code, ErrorMessage = message };
    }
    public Response Failure(HttpStatusCode httpStatusCode , string message)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)httpStatusCode;
        return new Response { IsSuccess = false,  ErrorMessage = message };
    }
    public Response Failure(HttpStatusCode httpStatusCode ,string code, string message)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)httpStatusCode;
        return new Response { IsSuccess = false, ErrorCode = code, ErrorMessage = message };
    }
}