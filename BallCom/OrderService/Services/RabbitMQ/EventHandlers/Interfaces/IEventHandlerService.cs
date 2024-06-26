using Shared.Models;
using Shared.Models.Customer;
using Shared.Models.Order;

namespace OrderService.Services.RabbitMQ.EventHandlers.Interfaces;

public interface IEventHandlerService
{
    // Payment events
    Task ProcessPaymentCreatedEvent(OrderPayment payment);
    Task ProcessPaymentUpdatedEvent(OrderPayment payment);
    
    // Customer events
    Task ProcessCustomerCreatedEvent(OrderCustomer customer);
    Task ProcessCustomerUpdatedEvent(OrderCustomer customer);
    
    // Product events
    Task ProcessProductCreatedEvent(OrderProduct product);
    Task ProcessProductUpdatedEvent(OrderProduct product);
}