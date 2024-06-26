using OrderService.Domain;
using OrderService.DTO;
using Shared.Models;

namespace OrderService.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(Guid id);
        Task<IEnumerable<Order>> GetAllOrders();
        Task CreateOrder(OrderCreateDto order);
        
        Task UpdateOrder(Guid id, OrderUpdateDto order);
        Task UpdateOrderStatus(Guid id, OrderUpdateStatusDto order);
    }
}
