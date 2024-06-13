using InventoryService.Domain;
using InventoryService.Visitor.Interfaces;
using Shared.EventSourcing;

namespace InventoryService.Events
{
    public abstract class InventoryBaseEvent : Event
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public abstract void Accept(IVisitor @event);
    }
}
