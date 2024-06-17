using OrderService.Domain;

namespace OrderService.Services.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(Order order);
    }
}
