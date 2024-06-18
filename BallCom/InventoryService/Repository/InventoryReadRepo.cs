using InventoryService.Database;
using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class InventoryReadRepo : IReadRepository<Product>
    {
        private readonly InventoryDbContext context;

        public InventoryReadRepo(InventoryDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Product product)
        {
            context.Products.Add(product);
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

        public async Task UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await GetByIdAsync(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            existingProduct.Quantity = product.Quantity;

            await context.SaveChangesAsync();
        }
    }
}
