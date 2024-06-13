using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Shared.EventSourcing.Interfaces;
using Shared.Repository.Interface;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler : IInventoryEventHandler
    {
        private readonly IWriteRepository<InventoryBaseEvent> _repository;

        public InventoryEventHandler(IWriteRepository<InventoryBaseEvent> repository)
        {
            _repository = repository;
        }

        public void Handle(InventoryCreatedEvent @event)
        {
            // Save the event to seperate table in the database
            _repository.Save(@event);
        }

        public void Handle(InventoryRemoveEvent @event)
        {
            // Save the event to seperate table in the database
            _repository.Save(@event);
        }
    }
}
