using InventoryService.Events;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IProductEventHandler
    {
        Task Handle(ProductCreateEvent @event);
        Task Handle(ProductUpdateEvent @event);
    }
}
