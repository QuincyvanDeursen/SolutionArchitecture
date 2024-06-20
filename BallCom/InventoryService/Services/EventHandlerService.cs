using InventoryService.Database;
using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Events;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly InventoryDbContext _context;

        public EventHandlerService(InventoryDbContext inventoryDbContext)
        {
            _context = inventoryDbContext;   
        }
        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.FindAsync(product.Id);

            if (oldProduct == null)
            {
                throw new KeyNotFoundException();
            }

            var quantity = oldProduct.Quantity + product.Quantity;

            if (quantity < 0)
            {
                //TODO: cancel een update inventory en plaats bericht op bus met juiste label
            }

            _context.Products.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
    }
}
