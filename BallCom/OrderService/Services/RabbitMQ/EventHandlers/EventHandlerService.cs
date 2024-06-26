using OrderService.Services.RabbitMQ.EventHandlers.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models.Order;
using Shared.Models.Payment;
using Shared.Repository.Interface;

namespace OrderService.Services.RabbitMQ.EventHandlers
{
    public class EventHandlerService(
        IWriteRepository<OrderPayment> paymentWriteRepo,
        IReadRepository<Order> orderReadRepo,
        IWriteRepository<Order> orderWriteRepo,
        IWriteRepository<OrderCustomer> customerWriteRepo,
        IWriteRepository<OrderProduct> productWriteRepo,
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
            await messagePublisher.PublishAsync(order, "order.updated");
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
            if (order.Status == OrderStatus.Failed)
            {
                await messagePublisher.PublishAsync(order, "order.cancelled");
            }
            else
            {
                await messagePublisher.PublishAsync(order, "order.updated");
            }
        }

        public async Task ProcessOrderCancelledEvent(OrderPayment payment)
        {
            // 1. Update the payment
            await paymentWriteRepo.UpdateAsync(payment);
        }

        public async Task ProcessCustomerCreatedEvent(OrderCustomer customer)
        {
            // 1. Create a new customer (eventual consistency)
            await customerWriteRepo.CreateAsync(customer);
        }

        public async Task ProcessCustomerUpdatedEvent(OrderCustomer customer)
        {
            // 1. Update existing customer (eventual consistency)
            await customerWriteRepo.UpdateAsync(customer);
        }

        public async Task ProcessProductCreatedEvent(OrderProduct product)
        {
            // 1. Create a new product (eventual consistency)
            await productWriteRepo.CreateAsync(product);
        }

        public async Task ProcessProductUpdatedEvent(OrderProduct product)
        {
            // 1. Update existing product (eventual consistency)
            await productWriteRepo.UpdateAsync(product);
        }
    }
}
