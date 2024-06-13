using InventoryService.Domain;
using InventoryService.Events;
using Shared.EventSourcing.Interfaces;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler
    {
        public void Handle(InventoryCreatedEvent @event)
        {
            // SaveInventory(@event);
        }

        public void Handle(InventoryRemoveEvent @event)
        {
            // SaveInventory(@event);
        }
    }
}
