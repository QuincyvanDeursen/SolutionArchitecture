using InventoryManagement.Database;
using InventoryManagement.Domain;
using Shared.Repository.Interface;

namespace InventoryManagement.Repository
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
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
