using InventoryService.Domain;
using InventoryService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid id);
        Task AddProductToWriteDB(ProductCreateDto productCreateDto);
        Task AddProductToReadDB(Product product);
        Task UpdateProductToWriteDB(Guid id, ProductUpdateDto productUpdateDto);
        Task UpdateProductToReadDB(Guid id, Product product);
    }
}
