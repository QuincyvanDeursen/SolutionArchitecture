using InventoryService.Domain;

namespace InventoryService.Repository.Interface
{
    public interface IInventoryRepo
    {
        IEnumerable<Inventory> GetInventories();
        Inventory GetInventory(int id);
        void SaveInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(Inventory inventory);
    }
}
