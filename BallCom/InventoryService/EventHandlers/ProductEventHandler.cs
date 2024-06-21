using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.MessageBroker.Publisher.Interfaces;

namespace InventoryService.EventHandlers
{
    public class ProductEventHandler(IMessagePublisher messagePublisher) : IProductEventHandler
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(_messagePublisher));

        public async Task Handle(ProductCreateEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Product, "inventory.create");
        }

        public async Task Handle(ProductUpdateEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Product, "inventory.update");
        }
    }
}
