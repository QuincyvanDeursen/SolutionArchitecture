using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Repository.Interface;

namespace InventoryService.Endpoints
{
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IReadRepository<Inventory> _inventoryRepository;
        private readonly IInventoryEventHandler _eventHandler;

        public InventoryController(ILogger<InventoryController> logger, IReadRepository<Inventory> inventoryRepository,
            IInventoryEventHandler eventHandler)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
            _eventHandler = eventHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<Inventory>> Get()
        {
            return await _inventoryRepository.GetAllAsync();
        }

        [HttpGet]
        public async Task<Inventory> Get(Guid id)
        {
            return await _inventoryRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public void Post([FromBody] Inventory inventory)
        {
            var inventoryEvent = new InventoryCreatedEvent(inventory.ProductId, inventory.Quantity);

            // Save the event to seperate table in the database
            _eventHandler.Handle(inventoryEvent);

            // Save the inventory to the inventory table
            _inventoryRepository.CreateAsync(inventory);

        }

        [HttpDelete]
        public void Delete([FromBody] Inventory inventory)
        {
            var inventoryEvent = new InventoryRemoveEvent(inventory.ProductId, inventory.Quantity);

            // Save the event to seperate table in the database
            _eventHandler.Handle(inventoryEvent);

            // Remove the inventory from the inventory table
            _inventoryRepository.RemoveAsync(inventory);
        }
    }
}