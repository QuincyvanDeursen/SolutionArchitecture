using InventoryManagement.Domain;
using System.Text.Json;

namespace InventoryManagement.Events
{
    public abstract class Event
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; private set; }
        public string EventType { get; private set; }
        public string Data { get; private set; }
        public DateTime EventTime { get; private set; }
        public Product Product { get;  set;}
        protected Event()
        {
            // EF vereist een parameterloze constructor
        }

        protected Event(Guid aggregateId, Product data)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            EventType = GetType().Name;
            Data = JsonSerializer.Serialize(data);
            Product = data;
            EventTime = DateTime.UtcNow;
        }
    }
}
