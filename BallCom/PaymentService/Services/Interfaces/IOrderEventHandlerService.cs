using PaymentService.Domain;

namespace PaymentService.Services.Interfaces
{
    public interface IOrderEventHandlerService
    {
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
