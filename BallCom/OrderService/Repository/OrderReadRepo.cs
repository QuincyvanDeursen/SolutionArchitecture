using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using Shared.Models;
using Shared.Models.Order;
using Shared.Repository.Interface;

namespace OrderService.Repository;

public class OrderReadRepo(OrderDbContext context) : IReadRepository<Order>
{
    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await context.Orders.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Payment)
            .ToListAsync();
    }
}