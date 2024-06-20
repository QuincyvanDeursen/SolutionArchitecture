using OrderService.Events;

namespace OrderService.EventHandlers.Interfaces
{
    public interface IOrderEventHandler
    {
        Task Handle(OrderCreatedEvent @event);
        Task Handle(OrderUpdateEvent @event);

    }
}
