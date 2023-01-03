namespace Malfurion.Web.Exception;
public class BadRequest : System.Exception
{
    public BadRequest(string message) : base(message)
    {
    }
}