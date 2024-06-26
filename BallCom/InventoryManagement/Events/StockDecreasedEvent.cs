using InventoryManagement.Domain;

namespace InventoryManagement.Events
{
    public class StockDecreasedEvent : Event
    {
        public StockDecreasedEvent()
        {
            // EF vereist een parameterloze constructor
        }

        public StockDecreasedEvent(Guid aggregateId, Product product)
            : base(aggregateId, product)
        {
        }
    }
}
