using InventoryService.Domain;
using InventoryService.Domain.Enum;
using Shared.EventHandler;

namespace InventoryService.EventHandlers
{
    public class InventoryEventHandler : EventSourceHandler<InventoryEvent>
    {
        public override void Handle(InventoryEvent @event)
        {
            switch (@event.EventType)
            {
                case InventoryEventEnum.InventoryAdded:
                    // Add inventory
                    break;
                case InventoryEventEnum.InventoryRemoved:
                    // Remove inventory
                    break;
                default:
                    break;
            }
        }
    }
}
