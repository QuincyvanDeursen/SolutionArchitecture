using OrderService.EventHandlers.Interfaces;
using OrderService.Events;
using Shared.MessageBroker.Publisher.Interfaces;

namespace OrderService.EventHandlers
{
    public class OrderEventHandler : IOrderEventHandler
    {
        private readonly IMessagePublisher _messagePublisher;

        public OrderEventHandler(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher ?? throw new System.ArgumentNullException(nameof(_messagePublisher));
        }

        public async Task Handle(OrderCreatedEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Order, "order.create");
        }

        public async Task Handle(OrderCancelledEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Order, "order.cancelled");
        }
    }
}
