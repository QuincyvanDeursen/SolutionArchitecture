using InventoryService.Domain;
using InventoryService.Domain.Enum;
using InventoryService.Repository.Interface;
using InventoryService.Services.Interface;

namespace InventoryService.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepo _inventoryRepository;
        private readonly IInventoryEventRepo _inventoryEventRepository;

        public InventoryService(IInventoryRepo inventoryRepository, IInventoryEventRepo inventoryEventRepository)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryEventRepository = inventoryEventRepository;
        }

        public void AddInventory(Inventory input)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing

            // _inventoryRepository.SaveInventory();
            // _inventoryEventRepository.SaveInventory();
        }

        public void RemoveInventory(Inventory input)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing
        }

        public void UpdateInventory(Inventory input)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing
        }
    }
}