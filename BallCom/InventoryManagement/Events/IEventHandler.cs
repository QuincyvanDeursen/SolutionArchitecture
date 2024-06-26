using InventoryManagement.Domain;

namespace InventoryManagement.Events
{
    public interface IEventHandler
    {
        Task HandleProductCreatedAsync(Product product);

        Task HandleProductUpdatedAsync(Product product);

        Task HandleStockIncreasedAsync(Guid aggregateId);

        Task HandleStockDecreasedAsync(Guid aggregateId);
    }
}
