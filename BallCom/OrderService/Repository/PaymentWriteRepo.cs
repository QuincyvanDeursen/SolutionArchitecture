using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using Shared.Models;
using Shared.Models.Order;
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
            var oldPayment = await context.Payments.FirstOrDefaultAsync(p => p.Id == entity.Id);
            if(oldPayment == null)
            {
                throw new KeyNotFoundException("Payment was not found in the order database");
            }
            
            // Update only changeable fields
            oldPayment.Status = entity.Status;
            
            context.Payments.Update(oldPayment);
            await context.SaveChangesAsync();
        }
    }
}
