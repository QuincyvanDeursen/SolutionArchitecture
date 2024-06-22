using OrderService.Domain;

namespace OrderService.Services.Interface
{
    public interface IEventHandlerService
    {
        Task ProcessPaymentCreatedEvent(Payment payment);
        Task ProcessProductUpdatedEvent(Payment payment);
    }
}
