using Polly;
using Polly.Retry;
using RabbitMQ.Client;

namespace Shared.MessageBroker;

public class RabbitMqConnectionProvider(string uri) : IConnectionProvider
{
    private IConnection _connection ;
    
    private IConnectionFactory _connectionFactory = new ConnectionFactory()
    {
        Uri = new Uri(uri)
    };
    
    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.WriteLine("Error connecting to RabbitMQ. Retrying in 5 sec."); })
                .ExecuteAsync(async () =>
                {
                    _connection = await _connectionFactory.CreateConnectionAsync();
                });
        }
       
        return _connection;
    }
}