using InventoryService.Domain;
using Shared.Event;
using static InventoryService.Events.InventoryEventEnum;

namespace InventoryService.Events
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(InventoryEventEnum eventType, string data)
        {
            EventType = eventType.ToString();
            EventTimestamp = DateTime.Now;
            Data = data;
        }
    }
}
