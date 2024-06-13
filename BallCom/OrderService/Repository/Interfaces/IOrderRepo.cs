using OrderService.Domain;

namespace OrderService.Repository.Interface
{
    public interface IOrderRepo
    {
            IEnumerable<Order> GetOrders();
            Order GetOrder(int id);
            void SaveOrder(Order order);
            void UpdateOrder(Order order);
            void DeleteOrder(Order order);
    }
}
