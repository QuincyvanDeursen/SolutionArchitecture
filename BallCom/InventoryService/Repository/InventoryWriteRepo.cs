using InventoryService.Database;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{   

    // This class is responsible for saving and deleting events in the database (Event sourcing)
    public class InventoryWriteRepo : IWriteRepository<InventoryBaseEvent>
    {
        private readonly InventoryDbContext _context;

        public InventoryWriteRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task Save(InventoryBaseEvent @event)
        {
            await _context.InventoryEvents.AddAsync(@event);
            await _context.SaveChangesAsync();
        }
    }
}
