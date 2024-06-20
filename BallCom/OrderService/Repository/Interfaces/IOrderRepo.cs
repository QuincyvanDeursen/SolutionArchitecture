using OrderService.Domain;

namespace OrderService.Repository.Interface
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrder(Guid id);
        Task CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
