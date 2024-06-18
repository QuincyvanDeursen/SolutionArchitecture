using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using Shared.EventSourcing;

namespace InventoryService.Events
{
    public class InventoryCreatedEvent : InventoryBaseEvent
    {
        public InventoryCreatedEvent(string product)
        {
            Product = product;
        }
        public override void Accept(IInventoryEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
