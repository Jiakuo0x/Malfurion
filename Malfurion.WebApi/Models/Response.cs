namespace Malfurion.WebApi.Models;

public class Response
{
    public bool IsSuccess { get; set; }
    public object? Payload { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

public class Response<T> : Response
{
    public new T? Payload { get; set; }
}