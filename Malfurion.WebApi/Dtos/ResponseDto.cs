namespace Malfurion.WebApi.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string? Code { get; set; }
    public string? ErrorInfo { get; set; }
}
public class ResponseDto<T> : ResponseDto
{
    public T? Data { get; set; }
}