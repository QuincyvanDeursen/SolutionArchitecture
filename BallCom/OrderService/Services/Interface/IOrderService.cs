using OrderService.Domain;
using OrderService.DTO;

namespace OrderService.Services.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderCreateDto order);
        Task<Order> GetOrderById(Guid id);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
