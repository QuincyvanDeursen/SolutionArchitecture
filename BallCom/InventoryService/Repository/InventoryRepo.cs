using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Repository.Interface;

namespace InventoryService.Repository
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly InventoryDbContext _context;

        public InventoryRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inventory> GetInventories()
        {
            return _context.Inventories.ToList();
        }

        public Inventory GetInventory(int id)
        {
            return _context.Inventories.First(i => i.Id == id);
        }

        public void SaveInventory(Inventory inventory)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing
            _context.Inventories.Add(inventory);

        }

        public void UpdateInventory(Inventory inventory)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing
            _context.Inventories.Update(inventory);
        }

        public void DeleteInventory(Inventory inventory)
        {
            // Add event to inventoryEvent table for CQRS and Event Sourcing
            _context.Inventories.Remove(inventory);
        }
    }
}
