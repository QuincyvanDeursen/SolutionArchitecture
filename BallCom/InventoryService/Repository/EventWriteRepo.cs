using InventoryService.Database;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{   

    // This class is responsible for saving and deleting events in the database (Event sourcing)
    public class EventWriteRepo : IWriteRepository<InventoryBaseEvent>
    {
        private readonly InventoryDbContext _context;

        public EventWriteRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(InventoryBaseEvent entity)
        {
            await _context.InventoryEvents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InventoryBaseEvent entity)
        {
            var product = entity.Product;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
