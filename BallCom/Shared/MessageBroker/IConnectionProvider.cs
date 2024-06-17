using RabbitMQ.Client;

namespace Shared.MessageBroker;

public interface IConnectionProvider
{
    Task<IConnection> GetConnectionAsync();
}