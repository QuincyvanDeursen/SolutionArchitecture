using InventoryService.EventHandlers.Interfaces;

namespace InventoryService.Events
{
    public class InventoryUpdateEvent : InventoryBaseEvent
    {
        public InventoryUpdateEvent(string product)
        {
            Product = product;
        }
        public override void Accept(IInventoryEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
