using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace OrderService.Services.RabbitMQ;

public class OrderMessageListenerService(IMessageConsumer messageConsumer) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "order.*",
            "inventory.*", // TODO: remove this line (testing)
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // TODO: Based on the topic, do the appropriate action
        //Console.WriteLine($"[{data.Timestamp}] Received message: ({data.Topic} - {data.Id})");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }
}