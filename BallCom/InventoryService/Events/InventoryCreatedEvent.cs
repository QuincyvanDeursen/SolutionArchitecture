using InventoryService.Domain;
using InventoryService.Visitor.Interfaces;
using Shared.EventSourcing;

namespace InventoryService.Events
{
    public class InventoryCreatedEvent : InventoryBaseEvent
    {
        public InventoryCreatedEvent(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        public override void Accept(IVisitor @event)
        {
            @event.Handle(this);
        }
    }
}
