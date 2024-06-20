using InventoryService.Domain;
using InventoryService.Dto;

namespace InventoryService.Services.Interfaces
{
    public interface IEventHandlerService
    {
        Task ProcessProductCreatedEvent(Product product);
        Task ProcessProductUpdatedEvent(Product product);
    }
}
