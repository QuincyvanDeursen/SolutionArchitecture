using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;
using System.Text.Json;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler(IMessagePublisher messagePublisher) : IInventoryEventHandler
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(_messagePublisher));

        public async Task Handle(InventoryCreatedEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Product, "inventory.create");
        }

        public async Task Handle(InventoryUpdateEvent @event)
        {
            await _messagePublisher.PublishAsync(@event.Product, "inventory.update");
        }
    }
}
