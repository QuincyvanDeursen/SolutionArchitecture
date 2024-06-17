using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler : IInventoryEventHandler
    {
        private readonly IWriteRepository<InventoryBaseEvent> _inventoryWriteRepo;

        public InventoryEventHandler(IWriteRepository<InventoryBaseEvent> inventoryWriteRepo)
        {
            _inventoryWriteRepo = inventoryWriteRepo ?? throw new System.ArgumentNullException(nameof(inventoryWriteRepo));
        }

        public async Task Handle(InventoryCreatedEvent @event)
        {
            await _inventoryWriteRepo.Save(@event);
        }

        public async  Task Handle(InventoryUpdateEvent @event)
        {
            await _inventoryWriteRepo.Save(@event);
        }
    }
}
