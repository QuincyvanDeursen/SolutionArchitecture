using OrderService.Database;
using OrderService.Domain;
using Shared.Repository.Interface;

namespace OrderService.Repository
{
    public class PaymentWriteRepo(AppDbContext context) : IWriteRepository<Payment>
    {
        public async Task CreateAsync(Payment entity)
        {
            //Get order and update paymentId
            var order = await context.Orders.FindAsync(entity.OrderId);
            order.PaymentId = entity.Id;
            context.Orders.Update(order);

            //Save payment to DB
            await context.Payments.AddAsync(entity);

            //Save all changes
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            var oldPayment = await context.Payments.FindAsync(entity.Id);
            var order = await context.Orders.FindAsync(entity.OrderId);

            if (oldPayment == null || order == null)
            {
                throw new KeyNotFoundException();
            }

            oldPayment.Status = entity.Status;
            order.OrderStatus = GetOrderStatus(entity.Status);

            context.Payments.Update(oldPayment);
            await context.SaveChangesAsync();
        }

        private OrderStatus GetOrderStatus(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Paid:
                    return OrderStatus.Paid;
                case PaymentStatus.Cancelled:
                    return OrderStatus.Cancelled;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
