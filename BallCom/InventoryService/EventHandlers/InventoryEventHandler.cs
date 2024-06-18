using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler : IInventoryEventHandler
    {
        private readonly IWriteRepository<InventoryBaseEvent> _inventoryWriteRepo;
        private readonly IMessagePublisher _messagePublisher;

        public InventoryEventHandler(IWriteRepository<InventoryBaseEvent> inventoryWriteRepo, IMessagePublisher messagePublisher)
        {
            _inventoryWriteRepo = inventoryWriteRepo ?? throw new System.ArgumentNullException(nameof(inventoryWriteRepo));
            _messagePublisher = messagePublisher ?? throw new System.ArgumentNullException(nameof(_messagePublisher));
        }

        public async Task Handle(InventoryCreatedEvent @event)
        {
            await _inventoryWriteRepo.Save(@event);
            await _messagePublisher.PublishAsync(new { Message = @event.Product }, "inventory.create");
        }

        public async  Task Handle(InventoryUpdateEvent @event)
        {
            await _inventoryWriteRepo.Save(@event);
            await _messagePublisher.PublishAsync(new { Message = @event.Product }, "inventory.update");
        }
    }
}
