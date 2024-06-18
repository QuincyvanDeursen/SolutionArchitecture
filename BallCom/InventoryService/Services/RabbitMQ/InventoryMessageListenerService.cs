

using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace InventoryService.Services.RabbitMQ;

public class InventoryMessageListenerService(IMessageConsumer messageConsumer) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync<MessageEventData<object>>(OnMessageReceived, new []
        {
            "inventory.create",
            "inventory.update"
        });
    }

    public async Task OnMessageReceived(MessageEventData<object> data)
    {
        switch (data.Topic)
        {
            case "inventory.create":
                Console.WriteLine();
                break;
            case "inventory.update":
                Console.WriteLine();
                // Huidige inventory ophalen
                // Huidige inventory >= event.inventory
                // Zo ja, write db update
                // Zo nee, Error, inventory niet goed
                break;
            default:
                Console.WriteLine();
                throw new ArgumentException(data.Topic + " is not a listed topic.");
        }
            
        // TODO: Based on the topic, do the appropriate action
        Console.WriteLine($"[{data.Timestamp}] Received message: ({data.Topic} - {data.Id})");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }


}