using InventoryManagement.Database;
using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Interface;

namespace InventoryManagement.Repository
{
    public class ProductReadRepo : IReadRepository<Product>
    {
        private readonly AppDbContext _context;
        private ILogger<ProductReadRepo> _logger;


        public ProductReadRepo(AppDbContext appDbContext, ILogger<ProductReadRepo> logger)
        {
            _context = appDbContext;
            _logger = logger;

        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<IEnumerable<Product>> GetAllByIdAsync(Guid aggergateId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            try
            {
                var list = await _context.Products.ToListAsync();
                foreach (var item in list)
                {
                    _logger.LogInformation("Product Id: {0}, Product Name: {1}", item.Id, item.Name);
                }
                return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Product not found", e);
                throw new Exception("Product not found", e);
            }
        }
    }
}
