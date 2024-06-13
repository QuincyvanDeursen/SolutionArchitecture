using OrderService.Database;
using OrderService.Domain;
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

        public Order GetOrder(int id)
        {
            return _context.Orders.First(i => i.Id == id);

        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public void SaveOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
