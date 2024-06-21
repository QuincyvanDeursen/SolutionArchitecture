

using InventoryService.Domain;
using InventoryService.Services.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace InventoryService.Services.RabbitMQ;

public class ProductMessageListenerService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider) : IHostedService
{
    private readonly IMessageConsumer _messageConsumer = messageConsumer;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await messageConsumer.ConsumeAsync(OnMessageReceived, new []
        {
            "inventory.create",
            "inventory.update",
            "order.create"
        });
    }

    public async Task OnMessageReceived(MessageEventData data)
    {
        // Create a new scope for the event handler service (scoped service)
        using var scope = _serviceProvider.CreateScope();
        var eventHandlerService = scope.ServiceProvider.GetRequiredService<IEventHandlerService>();  
        
        switch (data.Topic)
        {
            case "inventory.create":
                var createProduct = JsonSerializer.Deserialize<Product>(data.DataJson);

                await eventHandlerService.ProcessProductCreatedEvent(createProduct);
                break;
            case "inventory.update":
                var updateProduct = JsonSerializer.Deserialize<Product>(data.DataJson);
                
                await eventHandlerService.ProcessProductUpdatedEvent(updateProduct);
                break;
            default:
                throw new ArgumentException(data.Topic + " is not a subscribed topic.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }
}