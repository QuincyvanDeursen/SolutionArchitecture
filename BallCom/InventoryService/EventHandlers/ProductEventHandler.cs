using InventoryService.Domain;
using InventoryService.Endpoints;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.EventHandlers
{
    public class ProductEventHandler : IProductEventHandler
    {
        private readonly IReadRepository<Inventory> _inventoryRepository;
        private readonly IInventoryEventHandler _eventHandler;

        public ProductEventHandler(IReadRepository<Inventory> inventoryRepository, IInventoryEventHandler eventHandler)
        {
            _inventoryRepository = inventoryRepository;
            _eventHandler = eventHandler;
        }
        public async void Handle(Product product)
        {
            var inventory = new Inventory
            {
                ProductId = product.Id,
                Quantity = 0
            };

            var inventoryEvent = new InventoryCreatedEvent(inventory.ProductId, inventory.Quantity);

            _eventHandler.Handle(inventoryEvent);
            
            await _inventoryRepository.CreateAsync(inventory);
        }
    }
}
