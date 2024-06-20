using InventoryService.Events;
using Shared.EventSourcing;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IProductEventHandler
    {
        Task Handle(ProductCreateEvent @event);
        Task Handle(ProductUpdateEvent @event);
    }
}
