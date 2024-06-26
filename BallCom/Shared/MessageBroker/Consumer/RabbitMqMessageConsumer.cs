using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.MessageBroker.Connection;
using Shared.MessageBroker.Consumer.Interfaces;

namespace Shared.MessageBroker.Consumer;


// TODO: Recreate the plain rabbitmq consumer with multiple routing key support
public class RabbitMqMessageConsumer : IMessageConsumer
{
    private readonly IConnectionProvider _connectionProvider;
    private readonly string _exchangeName;
    private readonly string _queueName;

    private Lazy<Task<IConnection>> _connection;
    private Lazy<Task<IChannel>> _channel;

    public RabbitMqMessageConsumer(IConnectionProvider connectionProvider, string exchangeName, string queueName)
    {
        _connectionProvider = connectionProvider;
        _exchangeName = exchangeName;
        _queueName = queueName;

        _connection = new Lazy<Task<IConnection>>(CreateConnectionAsync);
        _channel = new Lazy<Task<IChannel>>(CreateChannelAsync);
    }
    
    private async Task<IConnection> CreateConnectionAsync()
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


    public async Task ConsumeAsync(Func<MessageEventData, Task> onMessageReceived, IEnumerable<string> interestedTopics)
    {
        var channel = await _channel.Value;
        
        // Declare the queue
        await channel.QueueDeclareAsync(queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        // Bind the interested topics to the queue and exchange
        foreach (var topic in interestedTopics)
        {
            await channel.QueueBindAsync(queue: _queueName, exchange: _exchangeName, routingKey: topic);
        }
        
        // Create the consumer
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (sender, e) =>
        {
            var body = e.Body.ToArray();
            var eventData = JsonSerializer.Deserialize<MessageEventData>(Encoding.UTF8.GetString(body));
            
            Console.WriteLine($"[{eventData.EventTimestamp}] Received message from bus (id -> {eventData.Id},topic -> {eventData.Topic})");
            Console.WriteLine($"[{eventData.EventTimestamp}] Payload: {eventData.DataJson}");
            
            await onMessageReceived(eventData);
        };
        
        // Start consuming
        await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
    }

    public async Task DisconnectAsync()
    {
        (await _connection.Value).Dispose();
    }
}