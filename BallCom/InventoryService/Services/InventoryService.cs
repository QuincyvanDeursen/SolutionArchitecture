using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using InventoryService.Services.Interfaces;
using Newtonsoft.Json;
using Shared.Repository.Interface;

namespace InventoryService.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IReadRepository<Product> _productReadRepo;
        private readonly IInventoryEventHandler _inventoryEventHandler;

        public InventoryService(IReadRepository<Product> productReadRepo, IInventoryEventHandler inventoryEventHandler)
        {
            _productReadRepo = productReadRepo;
            _inventoryEventHandler = inventoryEventHandler;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _productReadRepo.GetAllAsync();
            if (products == null || !products.Any())
            {
                throw new NullReferenceException();
            }

            return products;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var result = await _productReadRepo.GetByIdAsync(id);
            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result;
        }

        public async Task AddProduct(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreateDto.Name,
                Quantity = productCreateDto.Quantity,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description
            };

            var productJson = JsonConvert.SerializeObject(product);
            var inventoryEvent = new InventoryCreatedEvent(productJson);

            // Save the event to seperate table in the database
            await _inventoryEventHandler.Handle(inventoryEvent);
        }

        public async Task UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            var oldProduct = await _productReadRepo.GetByIdAsync(id);
            if (oldProduct == null)
            {
                throw new NullReferenceException("Product not found");
            }

            var product = new Product
            {
                Id = id,
                Name = productUpdateDto.Name ?? oldProduct.Name,
                Quantity = productUpdateDto.Quantity,
                Price = productUpdateDto.Price ?? oldProduct.Price,
                Description = productUpdateDto.Description ?? oldProduct.Description
            };


            var productJson = JsonConvert.SerializeObject(product);

            var inventoryEvent = new InventoryUpdateEvent(productJson);

            // Save the event to seperate table in the database
            await _inventoryEventHandler.Handle(inventoryEvent);

            // Update the product in the read table

        }
    }
}
