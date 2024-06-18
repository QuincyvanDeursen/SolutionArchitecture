using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;

namespace InventoryService.Events
{
    public class InventoryUpdateEvent : InventoryBaseEvent
    {
        public InventoryUpdateEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public InventoryUpdateEvent(Product product, string productJson)
        {
            Product = product;
            ProductJson = productJson;
        }
        public string ProductJson { get; set; }
        public override void Accept(IInventoryEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
