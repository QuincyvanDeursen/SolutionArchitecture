using OrderService.Database;
using Shared.Models;
using Shared.Repository.Interface;

namespace OrderService.Repository;

public class OrderWriteRepo(OrderDbContext context) : IWriteRepository<Order>
{
    public async Task CreateAsync(Order entity)
    {
        await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync();
    }
}