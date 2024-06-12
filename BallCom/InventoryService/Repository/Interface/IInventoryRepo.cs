using InventoryService.Domain;
using InventoryService.Events;

namespace InventoryService.Repository.Interface
{
    public interface IInventoryRepo
    {
        IEnumerable<InventoryEvent> GetEvents(int productId);
        void SaveEvent(InventoryEvent inventoryEvent);
        Inventory GetInventory(int productId);
    }
}
