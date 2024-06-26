using OrderService.Domain;
using OrderService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Repository.Interface;

namespace OrderService.Services.RabbitMQ.EventHandlers
{
    public class EventHandlerService(
        IWriteRepository<OrderPayment> paymentWriteRepo,
        IReadRepository<Order> orderReadRepo,
        IWriteRepository<Order> orderWriteRepo,
        IMessagePublisher messagePublisher
    ) : IEventHandlerService
    {
        public async Task ProcessPaymentCreatedEvent(OrderPayment payment)
        {
            // 1. Create a new payment (eventual consistency)
            await paymentWriteRepo.CreateAsync(payment);
            
            // 2. Populate the payment id in the order
            var order = await orderReadRepo.GetByIdAsync(payment.OrderId);
            order.PaymentId = payment.Id;
            
            await orderWriteRepo.UpdateAsync(order);
            
            // 3. Send a message that a order has been updated
            await messagePublisher.PublishAsync(order, "order.update");
        }

        public async Task ProcessPaymentUpdatedEvent(OrderPayment payment)
        {
            // 1. Update the payment
            await paymentWriteRepo.UpdateAsync(payment);
            
            // 2. Update the order status (based on the payment status match)
            var order = await orderReadRepo.GetByIdAsync(payment.OrderId);
            order.Status = payment.Status switch
            {
                PaymentStatus.Paid => OrderStatus.Processing,
                PaymentStatus.Cancelled => OrderStatus.Failed
            };
            await orderWriteRepo.UpdateAsync(order);
            
            // 3. Publish the order status update event
            await messagePublisher.PublishAsync(order, "order.update");
        }
    }
}
