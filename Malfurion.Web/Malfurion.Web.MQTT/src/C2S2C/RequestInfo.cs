namespace Malfurion.Web.MQTT.C2S2C;
public class RequestInfo
{
    public Guid RequestId { get; init; } = Guid.NewGuid();
    public string Payload { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public bool IsResponded { get; set; } = false;
    public DateTime RequestTime { get; init; } = DateTime.Now;
}