using InventoryService.Domain;
using InventoryService.Endpoints;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Repository.Interface;

namespace InventoryService.Controllers
{
    public class ProductController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IReadRepository<Product> _productRepository;

        public ProductController(ILogger<ProductController> logger, IReadRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAllAsync();
        }

        [HttpGet]
        public async Task<Product> Get(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            // Save the inventory to the inventory table
            _productRepository.CreateAsync(product);
        }

        [HttpDelete]
        public void Delete([FromBody] Product product)
        {
            // Remove the inventory from the inventory table
            _productRepository.RemoveAsync(product);
        }
    }
}
