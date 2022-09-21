namespace Malfurion.WebApi.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public string? Code { get; set; }
    public string? ErrorInfo { get; set; }

    [JsonIgnore]
    public int HttpCode { get; set; }
}