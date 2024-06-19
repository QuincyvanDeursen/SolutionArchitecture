using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Domain;
using OrderService.DTO;
using OrderService.Repository.Interface;

namespace OrderService.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderDbContext _context;
        public OrderRepo(OrderDbContext context)
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
            return _context.Orders.Include(order => order.OrderItems).First(i => i.Id == id);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public async Task<Order> SaveOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
