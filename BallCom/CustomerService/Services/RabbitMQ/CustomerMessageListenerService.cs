using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace CustomerService.Services.RabbitMQ;

public class CustomerMessageListenerService(IMessageConsumer messageConsumer) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync<MessageEventData<object>>(OnMessageReceived, new []
        {
            "customer.*",
            "order.*" // TODO: remove this line (testing)
        });
    }

    public async Task OnMessageReceived(MessageEventData<object> data)
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