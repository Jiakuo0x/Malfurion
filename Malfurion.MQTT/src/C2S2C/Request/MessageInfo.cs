namespace Malfurion.Web.MQTT.C2S2C.Request;
public class MessageInfo
{
    public Guid RequestId { get; init; } = Guid.NewGuid();

    public string RequestFromClientId { get; set; } = string.Empty;
    public string RequestToClientId { get; set; } = string.Empty;

    public string RequestTopic { get; set; } = string.Empty;
    public string ResponseTopic { get; set; } = string.Empty;

    public string RequestMessage { get; set; } = string.Empty;
    public string ResponseMessage { get; set; } = string.Empty;
    
    public bool IsResponded { get; set; } = false;
    public DateTime RequestTime { get; init; } = DateTime.Now;
}