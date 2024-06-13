using OrderService.Database;
using OrderService.Domain;
using OrderService.Repository.Interface;

namespace OrderService.Repository
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly OrderDbContext _context;

        public OrderItemRepo(OrderDbContext context) { 
            _context = context;
        }
        public void DeleteOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public OrderItem GetOrderItem(int id)
        {
           return  _context.OrderItems.First(i => i.Id == id);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItems.Where(i => i.OrderId == orderId).ToList();
        }

        public void SaveOrderItem(OrderItem orderItem)
        {
            _context.Add(orderItem);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }
    }
}
