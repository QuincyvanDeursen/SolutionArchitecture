using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Domain;
using OrderService.Repository.Interface;

namespace OrderService.Repository
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly AppDbContext _context;

        public OrderItemRepo(AppDbContext context) { 
            _context = context;
        }
        public void DeleteOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public async Task<OrderItem> GetOrderItem(Guid id)
        {
            return await _context.OrderItems.FirstAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(Guid orderId)
        {
            return _context.OrderItems.Where(i => i.OrderId == orderId).ToList();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public async Task<OrderItem> SaveOrderItem(OrderItem orderItem)
        {
            var savedOrderItem = await _context.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return savedOrderItem.Entity;
        }
    }
}
