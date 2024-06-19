using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace PaymentService.Services.RabbitMQ;

public class PaymentMessageListenerService(IMessageConsumer messageConsumer) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "payment.*"
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // TODO: Based on the topic, do the appropriate action
        Console.WriteLine($"[{data.Timestamp}] Received message: ({data.Topic} - {data.Id})");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }
}