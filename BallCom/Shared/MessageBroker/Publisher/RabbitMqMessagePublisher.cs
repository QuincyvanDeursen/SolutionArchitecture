using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Publisher.Interfaces;

namespace Shared.MessageBroker.Publisher;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly IConnectionProvider _connectionProvider;
    private readonly string _exchangeName;
    private readonly int _timeToLive;

    private Lazy<Task<IConnection>> _connection;
    private Lazy<Task<IChannel>> _channel;

    public RabbitMqMessagePublisher(IConnectionProvider connectionProvider, string exchangeName, int timeToLive = 30000)
    {
        _connectionProvider = connectionProvider;
        _exchangeName = exchangeName;
        _timeToLive = timeToLive;

        _connection = new Lazy<Task<IConnection>>(GetConnectionAsync);
        _channel = new Lazy<Task<IChannel>>(CreateChannelAsync);
    }
    
    private async Task<IConnection> GetConnectionAsync()
    {
        return await _connectionProvider.GetConnectionAsync();
    }

    private async Task<IChannel> CreateChannelAsync()
    {
        var connection = await _connectionProvider.GetConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchange: _exchangeName, type: ExchangeType.Topic);
        return channel;
    }
    
    public async Task PublishAsync<T>(T data, string topic)
    {
        var channel = await _channel.Value;
        var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new MessageEventData<T>()
        {
            Data = data,
            Topic = topic
        }));
        
        await channel.BasicPublishAsync(exchange: _exchangeName, routingKey: topic, body: messageBody);
    }
}