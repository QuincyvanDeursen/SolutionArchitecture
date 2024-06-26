using OrderService.Domain;

namespace OrderService.Services.RabbitMQ.EventHandlers.Interfaces;

public interface IEventHandlerService
{
    Task ProcessPaymentCreatedEvent(OrderPayment payment);
    Task ProcessPaymentUpdatedEvent(OrderPayment payment);
}