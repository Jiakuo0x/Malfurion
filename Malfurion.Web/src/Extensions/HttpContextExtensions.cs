namespace Malfurion.Web.Extensions;

internal static class Extensions
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