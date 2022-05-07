namespace Malfurion.WebApi.Services;
using Models;
public interface IResponseService
{
    Response Success();
    Response Success(object data);
    Response<T> Success<T>(T data);
    Response Failure();
    Response Failure(string message);
    Response Failure(string code, string message);
    Response Failure(HttpStatusCode httpStatusCode, string message);
    Response Failure(HttpStatusCode httpStatusCode, string code, string message);
}