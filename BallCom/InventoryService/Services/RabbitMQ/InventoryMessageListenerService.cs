

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
                Console.WriteLine();
                await CreateInventory(product);
                break;
            case "inventory.update":
                Console.WriteLine();
                await UpdateInventory(product.Id, product);
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

    private async Task CreateInventory(Product product)
    {
        var prodDto = new ProductCreateDto()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        };

        await _inventoryService.AddProduct(prodDto);
    }

    private async Task UpdateInventory(Guid id, Product product)
    {
        var oldProduct = await _inventoryService.GetProduct(id);

        var quantity = oldProduct.Quantity + product.Quantity;

        if (quantity < 0) return;

        var prodDto = new ProductUpdateDto
        {
            Quantity = quantity
        };

        await _inventoryService.UpdateProduct(id, prodDto);
    }
}