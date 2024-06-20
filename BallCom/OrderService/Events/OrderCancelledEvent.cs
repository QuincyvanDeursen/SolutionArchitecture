using OrderService.Domain;
using OrderService.EventHandlers.Interfaces;

namespace OrderService.Events
{
    public class OrderCancelledEvent : OrderBaseEvent
    {
        public OrderCancelledEvent()
        {
            // Parameterless constructor required by EF Core
        }

        public OrderCancelledEvent(Order order)
        {
            Order = order;
        }
        public override void Accept(IOrderEventHandler @event)
        {
            @event.Handle(this);
        }
    }
}
