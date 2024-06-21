using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;

namespace InventoryService.Events
{
    public class ProductUpdateEvent : ProductBaseEvent
    {
        public ProductUpdateEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public ProductUpdateEvent(Product product, string productJson)
        {
            Product = product;
            ProductJson = productJson;
        }
        public override void Accept(IProductEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
