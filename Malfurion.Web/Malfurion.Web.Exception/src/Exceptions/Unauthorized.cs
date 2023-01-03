namespace Malfurion.Web.Exception;
public class Unauthorized : System.Exception
{
    public Unauthorized(string message) : base(message)
    {
    }
}