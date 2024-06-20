using InventoryService.Database;
using InventoryService.Domain;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class ProductWriteRepo : IWriteRepository<Product>
    {
        private readonly InventoryDbContext _context;

        public ProductWriteRepo(InventoryDbContext context)
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

            if (oldProduct == null)
            {
                throw new KeyNotFoundException();
            }

            var quantity = oldProduct.Quantity + entity.Quantity;

            if (quantity < 0)
            {
                //TODO: cancel een update inventory en plaats bericht op bus met juiste label
            }

            _context.Products.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
    }
}
