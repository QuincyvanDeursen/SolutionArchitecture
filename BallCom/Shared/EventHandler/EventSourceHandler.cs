namespace Shared.EventHandler
{
    public abstract class EventSourceHandler<T>
    {
        public abstract void Handle(T @event);
    }
}
