namespace Shared.MessageBroker.Publisher.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync(object dataObj, string topic);
}