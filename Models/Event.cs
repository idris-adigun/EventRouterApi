namespace EventRouterApi.Models;

public class Event
{
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
}
