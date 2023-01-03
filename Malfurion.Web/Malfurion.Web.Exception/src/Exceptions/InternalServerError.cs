namespace Malfurion.Web.Exception;

public class InternalServerError : System.Exception
{
    public InternalServerError(string message) : base(message)
    {
    }
}