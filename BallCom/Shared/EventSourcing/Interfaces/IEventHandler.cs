namespace Shared.EventSourcing.Interfaces
{
    public interface IEventHandler<in T> where T : Event
    {
        Task Handle(T @event);
    }
}
