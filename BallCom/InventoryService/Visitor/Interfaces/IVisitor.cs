using InventoryService.Events;
using Shared.EventSourcing;

namespace InventoryService.Visitor.Interfaces
{
    public interface IVisitor
    {
        void Handle(InventoryCreatedEvent @event);
        void Handle(InventoryRemoveEvent @event);
    }
}
