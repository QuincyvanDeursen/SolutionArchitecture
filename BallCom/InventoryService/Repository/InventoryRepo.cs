using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Events;
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

        public IEnumerable<InventoryEvent> GetEvents(int productId)
        {
            return _context.InventoryEvents
                           .Where(e => e.ProductId == productId)
                           .OrderBy(e => e.EventTimestamp)
                           .ToList();
        }

        public Inventory GetInventory(int productId)
        {
            var events = GetEvents(productId);
            var inventory = new Inventory { ProductId = productId };

            foreach (var inventoryEvent in events)
            {
                switch (inventoryEvent.EventType)
                {
                    case InventoryEventEnum.InventoryAdded:
                        inventory.Quantity += inventoryEvent.Quantity;
                        break;
                    case InventoryEventEnum.InventoryRemoved:
                        inventory.Quantity -= inventoryEvent.Quantity;
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown event type: {inventoryEvent.GetType()}");
                }
            }
            return inventory;
        }

        public void SaveEvent(InventoryEvent inventoryEvent)
        {
            _context.InventoryEvents.Add(inventoryEvent);
            _context.SaveChanges();
        }
    }
}
