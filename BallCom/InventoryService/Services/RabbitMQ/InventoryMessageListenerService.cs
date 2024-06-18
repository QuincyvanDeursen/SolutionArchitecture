

using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Services.Interfaces;
using Shared.MessageBroker;
using Shared.MessageBroker.Consumer.Interfaces;

namespace InventoryService.Services.RabbitMQ;

public class InventoryMessageListenerService(IMessageConsumer messageConsumer, IInventoryService _inventoryService) : IHostedService
{
    private readonly IInventoryService _inventoryService = _inventoryService;
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
        var product = (Product)data.Data;

        switch (data.Topic)
        {
            case "inventory.create":
                await _inventoryService.AddProductToReadDB(product);
                break;
            case "inventory.update":
                await _inventoryService.UpdateProductToReadDB(product.Id, product);
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