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
        IWriteRepository<InventoryBaseEvent> eventWriteRepo,
        IWriteRepository<Product> productWriteRepo)
        : IEventHandlerService
    {
        public async Task ProcessProductCreatedEvent(Product product)
        {
            var productCreatedEvent = new InventoryCreatedEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await eventWriteRepo.CreateAsync(productCreatedEvent);
            await productWriteRepo.CreateAsync(product);
        }

        public async Task ProcessProductUpdatedEvent(Product product)
        {
            var productUpdatedEvent = new InventoryUpdateEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await eventWriteRepo.CreateAsync(productUpdatedEvent);
            await productWriteRepo.UpdateAsync(product);
        }
    }
}
