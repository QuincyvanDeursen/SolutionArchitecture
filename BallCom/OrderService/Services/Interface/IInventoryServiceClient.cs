using OrderService.Domain;

namespace OrderService.Services.Interface
{
    public interface IInventoryServiceClient
    {
        public Task<Product> GetInventoryAsync(Guid productId);
    }
}
