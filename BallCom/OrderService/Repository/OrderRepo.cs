using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Domain;
using OrderService.DTO;
using OrderService.Repository.Interface;

namespace OrderService.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }
        public void DeleteOrder(Order order)
        {     
            //Remove all order items associated with the order
            _context.OrderItems.RemoveRange(_context.OrderItems.Where(i => i.OrderId == order.Id));
            _context.Orders.Remove(order);
        }

        public async Task<Order> GetOrder(Guid id)
        {
            return await _context.Orders.Include(order => order.OrderItems).FirstAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
