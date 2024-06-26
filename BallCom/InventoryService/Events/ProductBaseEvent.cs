using InventoryService.Domain;
using InventoryService.EventHandlers.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Event;

namespace InventoryService.Events
{
    public abstract class ProductBaseEvent : Event
    {
        public string ProductJson { get; set; }
        public Product? Product { get; set; }
        public abstract void Accept(IProductEventHandler @event);
    }
}
