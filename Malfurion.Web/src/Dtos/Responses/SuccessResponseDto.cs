namespace Malfurion.Web.Dtos.Responses;

internal class SuccessResponseDto : ResponseDto
{
    public SuccessResponseDto() => base.IsSuccess = true;
}

internal class SuccessResponseDto<T> : SuccessResponseDto
{
    public SuccessResponseDto(T data) => this.Data = data;
    public T Data { get; set; }
}