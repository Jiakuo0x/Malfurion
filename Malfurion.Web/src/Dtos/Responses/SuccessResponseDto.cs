namespace Malfurion.Web.Dtos.Responses;

public class SuccessResponseDto : ResponseDto
{
    public SuccessResponseDto() => base.IsSuccess = true;
}

public class SuccessResponseDto<T> : SuccessResponseDto
{
    public SuccessResponseDto(T data) => this.Data = data;
    public T Data { get; set; }
}