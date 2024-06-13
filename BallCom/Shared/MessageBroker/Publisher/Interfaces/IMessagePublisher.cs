namespace Shared.MessageBroker.Publisher.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string topic);
}