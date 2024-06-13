using InventoryService.Events;
using Shared.EventSourcing;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IInventoryEventHandler
    {
        void Handle(InventoryCreatedEvent @event);
        void Handle(InventoryRemoveEvent @event);
    }
}
