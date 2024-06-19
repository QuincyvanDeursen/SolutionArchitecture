using InventoryService.Domain;
using InventoryService.Dto;

namespace InventoryService.Services.Interfaces
{
    public interface IEventHandlerService
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
    }
}
