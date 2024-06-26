using Shared.Models;
using Shared.Models.Payment;

namespace PaymentService.Services.Interfaces
{
    public interface IOrderEventHandlerService
    {
        Task ProcessOrderCreateEvent(PaymentOrder order);
        Task ProcessOrderUpdateEvent(PaymentOrder order);
    }
}
