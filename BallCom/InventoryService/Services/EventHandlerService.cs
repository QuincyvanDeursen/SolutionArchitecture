using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Events;
using InventoryService.Repository;
using InventoryService.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Shared.Repository.Interface;

namespace InventoryService.Services
{
    public class EventHandlerService(
        IWriteRepository<ProductBaseEvent> eventWriteRepo,
        IWriteRepository<Product> productWriteRepo,
        IReadRepository<Product> readRepository)
        : IEventHandlerService
    {
        public async Task ProcessOrderCreatedEvent(Order order)
        {
            if(order.OrderItems != null)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var oldProduct = await readRepository.GetByIdAsync(orderItem.ProductId);
                    oldProduct.Quantity -= orderItem.Quantity;
                    await ProcessProductUpdatedEvent(oldProduct);
                }
            }
        }

        public async Task ProcessProductCreatedEvent(Product product)
        {
            var productCreatedEvent = new ProductCreateEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await eventWriteRepo.CreateAsync(productCreatedEvent);
            await productWriteRepo.CreateAsync(product);
        }

        public async Task ProcessProductUpdatedEvent(Product product)
        {
            var productUpdatedEvent = new ProductUpdateEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await eventWriteRepo.CreateAsync(productUpdatedEvent);
            await productWriteRepo.UpdateAsync(product);
        }
    }
}
