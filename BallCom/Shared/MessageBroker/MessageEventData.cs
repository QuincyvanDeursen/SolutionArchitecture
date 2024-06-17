namespace Shared.MessageBroker;

public class MessageEventData<T>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Topic { get; set; }
    public T Data { get; set; }
}