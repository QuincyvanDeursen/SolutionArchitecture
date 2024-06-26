using Shared.Models;

namespace PaymentService.Services.Interfaces
{
    public interface IOrderEventHandlerService
    {
        Task ProcessOrderCreateEvent(PaymentOrder order);
        Task ProcessOrderUpdateEvent(PaymentOrder order);
    }
}
