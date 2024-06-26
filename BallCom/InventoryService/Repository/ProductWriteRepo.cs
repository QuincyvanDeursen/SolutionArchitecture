using InventoryService.Database;
using InventoryService.Domain;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class ProductWriteRepo : IWriteRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductWriteRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            var oldProduct = await _context.Products.FindAsync(entity.Id);
            if(oldProduct != null)
            {
                oldProduct.Quantity = entity.Quantity;

                _context.Products.Update(oldProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
