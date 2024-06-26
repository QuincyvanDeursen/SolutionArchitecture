using InventoryManagement.Database;
using InventoryManagement.Events;
using Shared.Repository.Interface;

namespace InventoryManagement.Repository
{
    public class ProductEventWriteRepo : IWriteRepository<Event>
    {
        private readonly AppDbContext _context;

        public ProductEventWriteRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Event entity)
        {
            await _context.ProductEvents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
