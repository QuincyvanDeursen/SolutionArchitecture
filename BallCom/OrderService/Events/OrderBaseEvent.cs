using OrderService.Domain;
using OrderService.EventHandlers.Interfaces;
using Shared.EventSourcing;

namespace OrderService.Events
{
    public abstract class OrderBaseEvent : Event
    {
        public string OrderJson { get; set; } = string.Empty;
        public Order? Order { get; set; }
        public abstract void Accept(IOrderEventHandler @event);
    }
}
