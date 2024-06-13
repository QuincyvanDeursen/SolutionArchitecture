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

        public async Task CreateAsync(Inventory entity)
        {
            var oldInventory = await context.Inventories.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (oldInventory != null)
            {
                oldInventory.Quantity += entity.Quantity;
            }
            else
            {
                context.Inventories.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Inventory entity)
        {
            var oldInventory = await context.Inventories.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (oldInventory != null)
            {
                oldInventory.Quantity -= entity.Quantity;
            }

            await context.SaveChangesAsync();
        }
    }
}
