using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;

namespace InventoryService.Events
{
    public class ProductCreateEvent : ProductBaseEvent
    {
        public ProductCreateEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public ProductCreateEvent(Product product, string productJson)
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
