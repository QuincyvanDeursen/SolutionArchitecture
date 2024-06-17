using InventoryService.Events;
using Shared.EventSourcing;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IInventoryEventHandler
    {
        Task Handle(InventoryCreatedEvent @event);
        Task Handle(InventoryUpdateEvent @event);
    }
}
