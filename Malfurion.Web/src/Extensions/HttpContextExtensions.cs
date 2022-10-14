namespace Malfurion.Web.Extensions;

public static class HttpContextExtensions
{
    public static bool SetHttpStatus(this HttpResponse httpResponse, HttpStatusCode httpStatusCode)
    {
        if(!httpResponse.HasStarted)
        {
            httpResponse.StatusCode = (int)httpStatusCode;
            return true;
        }
        else
        {
            return false;
        }
    }
}