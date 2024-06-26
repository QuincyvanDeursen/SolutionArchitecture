using InventoryManagement.Database;
using InventoryManagement.Events;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Interface;

namespace InventoryManagement.Repository
{
    public class ProductEventReadRepo : IReadRepository<Event>
    {
        private readonly AppDbContext _context;

        public ProductEventReadRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.ProductEvents.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllByIdAsync(Guid aggregateId)
        {
            return await _context.ProductEvents.Where(e => e.AggregateId == aggregateId).ToListAsync();
        }

        public Task<Event> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
