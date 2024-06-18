using Polly;
using RabbitMQ.Client;

namespace Shared.MessageBroker.Connection;

public class RabbitMqConnectionProvider(string uri) : IConnectionProvider
{
    private IConnection _connection ;
    
    private readonly ConnectionFactory _connectionFactory = new()
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