using InventoryService.Domain;

namespace InventoryService.Repository.Interface
{
    public interface IInventoryEventRepo
    {
        void SaveInventory(InventoryEvent test);
        void UpdateInventory(InventoryEvent inventoryEvent);
        void DeleteInventory(InventoryEvent inventoryEvent);
    }
}
