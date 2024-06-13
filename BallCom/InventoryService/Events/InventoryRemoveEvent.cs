using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using Shared.EventSourcing;

namespace InventoryService.Events
{
    public class InventoryRemoveEvent : InventoryBaseEvent
    {
        public InventoryRemoveEvent(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        public override void Accept(IInventoryEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
