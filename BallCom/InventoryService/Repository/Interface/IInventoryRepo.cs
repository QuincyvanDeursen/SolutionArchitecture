using InventoryService.Domain;

namespace InventoryService.Repository.Interface
{
    public interface IInventoryRepo
    {
        IEnumerable<Inventory> GetInventories();
        Inventory GetInventory(int id);
        void AddInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(int id);
    }
}
