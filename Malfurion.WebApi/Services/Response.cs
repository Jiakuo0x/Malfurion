namespace Malfurion.WebApi.Services;
public class Response
{
    private readonly HttpContext _httpContext;
    public Response(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public ResponseDto Ok()
    {
        _httpContext.Response.StatusCode = (int)StatusCodes.Status200OK;
        return new ResponseDto
        {
            IsSuccess = true,
        };
    }
    public ResponseDto Ok<T>(T data)
    {
        _httpContext.Response.StatusCode = (int)StatusCodes.Status200OK;
        return new ResponseDto<T>
        {
            IsSuccess = true,
            Data = data,
        };
    }
    public ResponseDto BadRequest(string errorInfo)
    {
        _httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
        return new ResponseDto
        {
            IsSuccess = false,
            ErrorInfo = errorInfo,
        };
    }

    public ResponseDto InternalServerError(string errorInfo)
    {
        _httpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        return new ResponseDto
        {
            IsSuccess = false,
            ErrorInfo = errorInfo,
        };
    }
}