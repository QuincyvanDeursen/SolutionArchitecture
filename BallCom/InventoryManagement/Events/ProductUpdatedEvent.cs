using InventoryManagement.Domain;
using System.Text.Json;

namespace InventoryManagement.Events
{
    public class ProductUpdatedEvent : Event
    {
        public ProductUpdatedEvent()
        {
            // EF vereist een parameterloze constructor
        }

        public ProductUpdatedEvent(Guid aggregateId, Product product)
            : base(aggregateId, product)
        {
        }
    }
}
