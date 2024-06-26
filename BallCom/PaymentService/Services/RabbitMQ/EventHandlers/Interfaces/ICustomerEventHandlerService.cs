using Shared.Models;
using Shared.Models.Payment;

namespace PaymentService.Services.RabbitMQ.EventHandlers.Interfaces;

public interface ICustomerEventHandlerService
{
    Task ProcessCustomerCreateEvent(PaymentCustomer customer);
    Task ProcessCustomerUpdateEvent(PaymentCustomer customer);
}