using OrderService.Domain;

namespace OrderService.Services.Interface
{
    public interface IInventoryServiceClient
    {
        public Task<Inventory> GetInventoryAsync(int productId);
    }
}
