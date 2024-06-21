using OrderService.Database;
using OrderService.Domain;
using Shared.Repository.Interface;

namespace OrderService.Repository
{
    public class PaymentRepo : IWriteRepository<Payment>
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Payment entity)
        {
            //Get order and update paymentId
            var order = await _context.Orders.FindAsync(entity.OrderId);
            order.PaymentId = entity.Id;
            _context.Orders.Update(order);

            //Save payment to DB
            await _context.Payments.AddAsync(entity);

            //Save all changes
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            var oldPayment = await _context.Payments.FindAsync(entity.Id);
            var order = await _context.Orders.FindAsync(entity.OrderId);

            if (oldPayment == null || order == null)
            {
                throw new KeyNotFoundException();
            }

            oldPayment.Status = entity.Status;
            order.OrderStatus = GetOrderStatus(entity.Status);

            _context.Payments.Update(oldPayment);
            await _context.SaveChangesAsync();
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
