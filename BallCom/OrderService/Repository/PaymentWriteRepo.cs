using OrderService.Database;
using OrderService.Domain;
using Shared.Models;
using Shared.Repository.Interface;

namespace OrderService.Repository
{
    public class PaymentWriteRepo(OrderDbContext context) : IWriteRepository<OrderPayment>
    {
        public async Task CreateAsync(OrderPayment entity)
        {
            await context.Payments.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderPayment entity)
        {
            context.Payments.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
