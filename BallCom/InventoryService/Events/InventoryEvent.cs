using Shared.Event;
using System.Linq.Expressions;

namespace InventoryService.Events
{
    public class InventoryEvent : Event
    {
        public InventoryEventEnum EventType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public InventoryEvent() { }

        public InventoryEvent(InventoryEventEnum inventoryEvent, int productId, int quantity)
        {
            this.EventType = inventoryEvent;
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
