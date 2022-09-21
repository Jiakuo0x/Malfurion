namespace Malfurion.WebApi.Services;
public class ResponseService
{
    public ResponseDto Result = new() { IsSuccess = true };
    public ResponseService()
    {
    }

    public ResponseDto Ok()
        => Result = new ResponseDto
        {
            IsSuccess = true,
            HttpCode = (int)HttpStatusCode.OK,
        };
    public ResponseDto Ok(object data)
        => Result = new ResponseDto
        {
            IsSuccess = true,
            Data = data,
            HttpCode = (int)HttpStatusCode.OK,
        };
    public ResponseDto BadRequest(string errorInfo)
        => Result = new ResponseDto
        {
            IsSuccess = false,
            ErrorInfo = errorInfo,
            HttpCode = (int)HttpStatusCode.BadRequest,
        };

    public ResponseDto InternalServerError(string errorInfo)
        => Result = new ResponseDto
        {
            IsSuccess = false,
            ErrorInfo = errorInfo,
            HttpCode = (int)HttpStatusCode.InternalServerError,
        };
}