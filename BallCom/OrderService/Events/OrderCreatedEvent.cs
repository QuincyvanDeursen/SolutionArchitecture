using OrderService.Domain;
using OrderService.EventHandlers.Interfaces;

namespace OrderService.Events
{
    public class OrderCreatedEvent : OrderBaseEvent
    {
        public OrderCreatedEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public OrderCreatedEvent(Order order, string productJson)
        {
            Order = order;
            OrderJson = productJson;
        }
        public override void Accept(IOrderEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
