using InventoryService.Domain.Enum;
using Shared.Event;
using System.Linq.Expressions;

namespace InventoryService.Domain
{
    public class InventoryEvent(InventoryEventEnum inventoryEvent, int productId, int quantity)
        : Event
    {
        public InventoryEventEnum EventType { get; set; } = inventoryEvent;
        public int ProductId { get; set; } = productId;
        public int Quantity { get; set; } = quantity;
    }
}