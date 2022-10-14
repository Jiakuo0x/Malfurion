namespace Malfurion.Web.Dtos.Responses;

public class FailureResponseDto : ResponseDto
{
    public FailureResponseDto(string errorMessage) => this.ErrorMessage = errorMessage;
    public FailureResponseDto(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    public string? ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}