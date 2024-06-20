using System.Text.Json;
using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using InventoryService.Services.Interfaces;
using Shared.Repository.Interface;

namespace InventoryService.Services
{
    public class ProductService : IProductService
    {
        private readonly IReadRepository<Product> _productReadRepo;
        private readonly IProductEventHandler _productEventHandler;
        private readonly ILogger<ProductService> _logger;


        public ProductService(IReadRepository<Product> productReadRepo, IProductEventHandler productEventHandler, ILogger<ProductService> logger)
        {
            _productReadRepo = productReadRepo;
            _productEventHandler = productEventHandler;
            _logger = logger;
        }

        // This method is used to get all products from the  read database
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _productReadRepo.GetAllAsync();
            if (products == null || !products.Any())
            {
                throw new NullReferenceException();
            }

            return products;
        }

        // This method is used to get a product by its id from the read database
        public async Task<Product> GetProduct(Guid id)
        {
            var result = await _productReadRepo.GetByIdAsync(id);
            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result;
        }

        // This method is used to create a new product and publish the event to the message broker
        public async Task SendCreateEvent(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreateDto.Name,
                Quantity = productCreateDto.Quantity,
                Price = productCreateDto.Price,
                Description = productCreateDto.Description
            };
            
            var inventoryEvent = new ProductCreateEvent(product, JsonSerializer.Serialize(product));
            
            Console.WriteLine($"{inventoryEvent.Id} - {inventoryEvent.EventTimestamp} - {inventoryEvent.ProductJson}");

            //Publish the event to the message broker
            await _productEventHandler.Handle(inventoryEvent);
        }

        // This method is used to update a product and publish the event to the message broker
        public async Task SendUpdateEvent(Guid id, ProductUpdateDto productUpdateDto)
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

            var productJson = JsonSerializer.Serialize(product);
            var inventoryEvent = new ProductUpdateEvent(oldProduct, productJson);

            //Publish the event to the message broker
            await _productEventHandler.Handle(inventoryEvent);
        }

        public async Task<bool> CheckStock(List<ProductStockDto> productsToCheck)
        {
            foreach (var product in productsToCheck)
            {
                var productInDb = await _productReadRepo.GetByIdAsync(product.ProductId);
                if (productInDb == null)
                {
                    _logger.LogInformation($"Product with id {product.ProductId} not found");
                    throw new Exception($"Product with id {product.ProductId} not found");
                }

                if (productInDb.Quantity < product.Quantity)
                {
                    _logger.LogInformation($"Product with id {product.ProductId} has insufficient stock");
                    return false;
                }
            }
            return true;
        }
    }
}
