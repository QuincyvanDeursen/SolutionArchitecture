using OrderService.Database;
using Shared.Models.Customer;
using Shared.Models.Order;
using Shared.Repository.Interface;

namespace OrderService.Repository;

public class CustomerWriteRepo(OrderDbContext context) : IWriteRepository<OrderCustomer>
{
    public async Task CreateAsync(OrderCustomer entity)
    {
        context.Customers.Add(entity);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(OrderCustomer entity)
    {
        context.Customers.Update(entity);
        return context.SaveChangesAsync();
    }
}