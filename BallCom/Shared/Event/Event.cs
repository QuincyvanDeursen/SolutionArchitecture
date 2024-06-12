namespace Shared.Event
{
    public abstract class Event
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime EventTimestamp { get; } = DateTime.Now;
    }
}
