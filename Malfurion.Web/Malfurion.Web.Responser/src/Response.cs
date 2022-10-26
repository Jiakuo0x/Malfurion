namespace Malfurion.Web.Responser;
public class Response
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Response(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ResponseDto Ok() => Custom(new SuccessResponseDto(), HttpStatusCode.OK);
    public ResponseDto Ok<T>(T data) => Custom(new SuccessResponseDto<T>(data), HttpStatusCode.OK);
    public ResponseDto File(Stream fileStream, string fileName) => Custom(new SuccessResponseDto<object>(
        new
        {
            FileStream = fileStream,
            FileName = fileName,
        }
    ), HttpStatusCode.OK);
    public ResponseDto BadRequest(string errorMessage) => Custom(new FailureResponseDto(errorMessage), HttpStatusCode.BadRequest);
    public ResponseDto InternalServerError(string errorMessage) => Custom(new FailureResponseDto(errorMessage), HttpStatusCode.InternalServerError);
    public ResponseDto Custom(ResponseDto responseDto, HttpStatusCode httpStatusCode)
    {
        _httpContextAccessor.HttpContext.Response.SetHttpStatus(httpStatusCode);
        return responseDto;
    }
}