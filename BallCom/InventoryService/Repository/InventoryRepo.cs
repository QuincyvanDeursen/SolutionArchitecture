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
            return _context.Inventories.FirstOrDefault(i => i.Id == id);
        }

        public void AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public void UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
        }

        public void DeleteInventory(int id)
        {
            var inventory = _context.Inventories.FirstOrDefault(i => i.Id == id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                _context.SaveChanges();
            }
        }
    }
}
