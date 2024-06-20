using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Events;
using InventoryService.Repository;
using InventoryService.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace InventoryService.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly EventWriteRepo _eventWriteRepo;
        private readonly ProductWriteRepo _productWriteRepo;

        public EventHandlerService(EventWriteRepo eventWriteRepo, ProductWriteRepo productWriteRepo)
        {
            this._eventWriteRepo = eventWriteRepo;
            _productWriteRepo = productWriteRepo;
        }

        public async Task ProcessProductCreatedEvent(Product product)
        {
            var productCreatedEvent = new InventoryCreatedEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await _eventWriteRepo.CreateAsync(productCreatedEvent);
            await _productWriteRepo.CreateAsync(product);
        }

        public async Task ProcessProductUpdatedEvent(Product product)
        {
            var productUpdatedEvent = new InventoryUpdateEvent
            {
                Product = product,
                ProductJson = JsonSerializer.Serialize(product)
            };

            await _eventWriteRepo.CreateAsync(productUpdatedEvent);
            await _productWriteRepo.UpdateAsync(product);
        }
    }
}
