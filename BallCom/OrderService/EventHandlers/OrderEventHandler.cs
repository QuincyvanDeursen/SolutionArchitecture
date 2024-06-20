using OrderService.EventHandlers.Interfaces;
using OrderService.Events;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

namespace OrderService.EventHandlers
{
    public class OrderEventHandler : IOrderEventHandler
    {
        private readonly IWriteRepository<OrderBaseEvent> _inventoryWriteRepo;
        private readonly IMessagePublisher _messagePublisher;

        public OrderEventHandler(IWriteRepository<OrderBaseEvent> inventoryWriteRepo, IMessagePublisher messagePublisher)
        {
            _inventoryWriteRepo = inventoryWriteRepo ?? throw new System.ArgumentNullException(nameof(inventoryWriteRepo));
            _messagePublisher = messagePublisher ?? throw new System.ArgumentNullException(nameof(_messagePublisher));
        }

        public async Task Handle(OrderCreatedEvent @event)
        {
            await _inventoryWriteRepo.CreateAsync(@event);
            await _messagePublisher.PublishAsync(@event.Order, "order.create");
        }

        public async Task Handle(OrderUpdateEvent @event)
        {
            await _inventoryWriteRepo.CreateAsync(@event);
            await _messagePublisher.PublishAsync(@event.Order, "order.update");
        }
    }
}
