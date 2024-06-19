namespace Shared.MessageBroker.Consumer.Interfaces;

public interface IMessageConsumer
{
    Task ConsumeAsync(Func<MessageEventData, Task> onMessageReceived, IEnumerable<string> interestedTopics);
}