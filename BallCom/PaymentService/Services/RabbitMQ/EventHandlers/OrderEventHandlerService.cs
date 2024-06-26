using PaymentService.Services.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Models.Payment;
using Shared.Repository.Interface;

namespace PaymentService.Services.RabbitMQ.EventHandlers
{
    public class OrderEventHandlerService(
        IWriteRepository<PaymentOrder> paymentOrderWriteRepository, 
        IWriteRepository<Payment> paymentWriteRepository,
        IMessagePublisher messagePublisher
    )
        : IOrderEventHandlerService
    {
        public async Task ProcessOrderCreateEvent(PaymentOrder order)
        {
            // 1. Create a new order (eventual consistency)
            await paymentOrderWriteRepository.CreateAsync(order);
            
            // 2. Create a new payment
            var payment = new Payment()
            {
                Id = Guid.NewGuid(),
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                Status = PaymentStatus.Pending,
            };
            await paymentWriteRepository.CreateAsync(payment);

            // 3. Publish new payment event to the message broker
            await messagePublisher.PublishAsync(payment, "payment.create");
        }

        public async Task ProcessOrderUpdateEvent(PaymentOrder order)
        {
            // 1. Update the order (eventual consistency)
            await paymentOrderWriteRepository.UpdateAsync(order);
        }
    }
}
