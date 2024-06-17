namespace Shared.MessageBroker.Consumer.Interfaces;

public interface IMessageConsumer
{
    Task ConsumeAsync<T>(Func<T, Task> onMessageReceived, IEnumerable<string> interestedTopics);
}