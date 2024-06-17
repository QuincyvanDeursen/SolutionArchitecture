using RabbitMQ.Client;

namespace Shared.MessageBroker.Connection;

public interface IConnectionProvider
{
    Task<IConnection> GetConnectionAsync();
}