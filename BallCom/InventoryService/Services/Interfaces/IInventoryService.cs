using InventoryService.Domain;
using InventoryService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid id);
        Task CreateEvent(ProductCreateDto productCreateDto);
        Task CreateUpdateEvent(Guid id, ProductUpdateDto productUpdateDto);
    }
}
