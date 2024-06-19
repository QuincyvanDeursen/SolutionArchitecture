

using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Services.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace InventoryService.Services.RabbitMQ;

public class InventoryMessageListenerService(IMessageConsumer messageConsumer, IEventHandlerService eventHandlerService) : IHostedService
{
    private readonly IEventHandlerService _eventHandlerService = eventHandlerService;
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
                var createProduct = (Product)data.Data;
                await _eventHandlerService.AddProduct(createProduct);
                break;
            case "inventory.update":
                var updateProduct = (Product)data.Data;
                await _eventHandlerService.UpdateProduct(updateProduct);
                break;
            default:
                Console.WriteLine();
                throw new ArgumentException(data.Topic + " is not a listed topic.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // TODO: Implement the stop logic for the connection
        return Task.CompletedTask;
    }
}