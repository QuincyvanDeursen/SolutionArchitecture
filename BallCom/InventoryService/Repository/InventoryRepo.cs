using InventoryService.Database;
using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class InventoryRepo(InventoryDbContext context) : IReadRepository<Inventory>
    {
        public async Task<Inventory> GetByIdAsync(Guid id)
        {
            return await context.Inventories.FindAsync(id);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await context.Inventories.ToListAsync();
        }
    }
}
