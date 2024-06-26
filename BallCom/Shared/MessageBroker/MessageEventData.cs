using System.Text.Json;

namespace Shared.MessageBroker;

public class MessageEventData() : Shared.Event.Event
{
    public string Topic { get; set; }
    public string DataJson { get; set; }
}