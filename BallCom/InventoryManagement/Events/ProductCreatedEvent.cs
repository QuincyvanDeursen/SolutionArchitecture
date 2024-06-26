using InventoryManagement.Domain;
using System.Text.Json;

namespace InventoryManagement.Events
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent()
        {
            // EF vereist een parameterloze constructor
        }

        public ProductCreatedEvent(Guid aggregateId, Product product)
            : base(aggregateId, product)
        {
        }
    }
}
