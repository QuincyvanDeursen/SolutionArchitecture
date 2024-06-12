using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Repository.Interface;

namespace InventoryService.Repository
{
    public class InventoryEventRepo : IInventoryEventRepo
    {
        private readonly InventoryDbContext _context;

        public InventoryEventRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public void SaveInventory(InventoryEvent inventoryEvent)
        {
            _context.InventoryEvents.Add(inventoryEvent);
        }

        public void UpdateInventory(InventoryEvent inventoryEvent)
        {
        }

        public void DeleteInventory(InventoryEvent inventoryEvent)
        {
        }
    }
}
