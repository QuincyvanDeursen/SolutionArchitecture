namespace Shared.Event
{
    public abstract class Event
    {
        public int Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public DateTime EventTimestamp { get;set; }
        public string Data { get; set; } = string.Empty;
    }
}
