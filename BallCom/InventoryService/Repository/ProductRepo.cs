using InventoryService.Database;
using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class ProductRepo(InventoryDbContext context) : IReadRepository<Product>
    {
        public async Task CreateAsync(Product entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);

        }

        public async Task RemoveAsync(Product entity)
        {
            context.Products.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
