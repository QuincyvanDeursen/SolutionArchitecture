using OrderService.Domain;
using OrderService.EventHandlers.Interfaces;

namespace OrderService.Events
{
    public class OrderUpdateEvent : OrderBaseEvent
    {
        public OrderUpdateEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public OrderUpdateEvent(Order order, string productJson)
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
