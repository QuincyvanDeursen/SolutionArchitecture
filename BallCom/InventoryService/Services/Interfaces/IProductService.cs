using InventoryService.Domain;
using InventoryService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid id);
        Task SendCreateEvent(ProductCreateDto productCreateDto);
        Task SendUpdateEvent(Guid id, ProductUpdateDto productUpdateDto);
        Task<bool> CheckStock (List<ProductStockDto> productsToCheck);
    }
}
