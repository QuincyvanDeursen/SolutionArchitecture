using InventoryService.Database;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{   

    // This class is responsible for saving and deleting events in the database (Event sourcing)
    public class ProductEventWriteRepo : IWriteRepository<ProductBaseEvent>
    {
        private readonly AppDbContext _context;

        public ProductEventWriteRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProductBaseEvent entity)
        {
            await _context.ProductEvents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductBaseEvent entity)
        {
            var product = entity.Product;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
