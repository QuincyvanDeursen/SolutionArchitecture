using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Domain;
using Shared.Repository.Interface;

namespace OrderService.Repository;

public class ProductReadRepo(OrderDbContext context) : IReadRepository<OrderProduct>
{
    public async Task<OrderProduct> GetByIdAsync(Guid id)
    {
        return await context.Products.FirstOrDefaultAsync(op => op.Id == id);
    }

    public async Task<IEnumerable<OrderProduct>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }
}