using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using Shared.EventSourcing;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Events
{
    public abstract class InventoryBaseEvent : Event
    {
        public string ProductJson { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public abstract void Accept(IInventoryEventHandler @event);
    }
}
