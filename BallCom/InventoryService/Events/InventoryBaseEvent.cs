using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using Shared.EventSourcing;

namespace InventoryService.Events
{
    public abstract class InventoryBaseEvent : Event
    {
        public string Product { get; set; } = string.Empty;
        public abstract void Accept(IInventoryEventHandler @event);
    }
}
