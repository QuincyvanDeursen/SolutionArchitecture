using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;

namespace InventoryService.Events
{
    public class InventoryCreatedEvent : InventoryBaseEvent
    {
        public InventoryCreatedEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public InventoryCreatedEvent(Product product, string productJson)
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
