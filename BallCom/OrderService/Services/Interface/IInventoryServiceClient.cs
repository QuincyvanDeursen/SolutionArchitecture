using OrderService.Domain;
using OrderService.DTO;

namespace OrderService.Services.Interface
{
    public interface IInventoryServiceClient
    {
        public Task<bool> CheckStockAsync(List<ProductStockDto> productsFromOrder);
    }
}
