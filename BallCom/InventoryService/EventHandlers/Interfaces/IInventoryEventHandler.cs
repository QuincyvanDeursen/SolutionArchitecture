using InventoryService.Events;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IInventoryEventHandler
    {
        Task Handle(InventoryCreatedEvent @event);
        Task Handle(InventoryUpdateEvent @event);
    }
}
