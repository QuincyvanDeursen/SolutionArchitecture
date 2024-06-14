using InventoryService.Domain;

namespace InventoryService.EventHandlers.Interfaces
{
    public interface IProductEventHandler
    {
        void Handle(Product product);
    }
}
