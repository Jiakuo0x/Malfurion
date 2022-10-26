namespace Malfurion.Web.Kong.Comm;

internal class CommBase
{
    public string AdminApiAddr { get; init; }
    public CommBase(string adminApiAddr)
    {
        AdminApiAddr = adminApiAddr;
    } 
}