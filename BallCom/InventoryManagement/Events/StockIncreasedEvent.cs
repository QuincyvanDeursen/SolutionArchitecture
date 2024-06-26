using InventoryManagement.Domain;

namespace InventoryManagement.Events
{
    public class StockIncreasedEvent : Event
    {
        public StockIncreasedEvent()
        {
            // EF vereist een parameterloze constructor
        }

        public StockIncreasedEvent(Guid aggregateId, Product product)
            : base(aggregateId, product)
        {
        }
    }
}
