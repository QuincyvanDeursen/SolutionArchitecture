using PaymentService.Database;
using PaymentService.Domain;
using PaymentService.Services.Interfaces;

namespace PaymentService.Services
{
    public class OrderEventHandlerService : IOrderEventHandlerService
    {
        private readonly PaymentDbContext _context;
        public OrderEventHandlerService(PaymentDbContext inventoryDbContext)
        {
            _context = inventoryDbContext;
        }

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateOrder(Order order)
        {
            var oldProduct = await _context.Orders.FindAsync(order.Id);

            if (oldProduct == null)
            {
                throw new KeyNotFoundException();
            }

            //TODO: implement logic for updating order

            _context.Orders.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
    }
}
