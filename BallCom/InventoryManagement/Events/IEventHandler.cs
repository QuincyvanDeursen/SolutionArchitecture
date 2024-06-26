using InventoryManagement.Domain;

namespace InventoryManagement.Events
{
    public interface IEventHandler
    {
        Task HandleProductCreatedAsync(Product product);

        Task HandlestockIncreasedAsync(Guid aggregateId);
    }
}
