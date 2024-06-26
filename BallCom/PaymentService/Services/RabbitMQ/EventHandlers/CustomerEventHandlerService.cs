using PaymentService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Repository.Interface;

namespace PaymentService.Services.RabbitMQ.EventHandlers;

public class CustomerEventHandlerService(IWriteRepository<PaymentCustomer> paymentCustomerWriteRepository, 
    IMessagePublisher messagePublisher) : ICustomerEventHandlerService
{
    public async Task ProcessCustomerCreateEvent(PaymentCustomer order)
    {
        // 1. Create a new customer (eventual consistency)
        await paymentCustomerWriteRepository.CreateAsync(order);
    }

    public Task ProcessCustomerUpdateEvent(PaymentCustomer customer)
    {
        // 2. Update the customer (eventual consistency)
        return paymentCustomerWriteRepository.UpdateAsync(customer);
    }
}