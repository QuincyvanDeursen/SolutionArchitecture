using System.Text.Json;

namespace Shared.MessageBroker;

public class MessageEventData()
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Topic { get; set; }
    public string DataJson { get; set; }
}