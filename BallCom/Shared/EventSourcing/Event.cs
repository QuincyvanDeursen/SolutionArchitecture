namespace Shared.EventSourcing
{
    public abstract class Event
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime EventTimestamp { get; } = DateTime.Now;

    }
}
