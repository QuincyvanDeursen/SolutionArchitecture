using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                _logger.LogInformation("Getting all products");

                var products = await _productService.GetAllProducts();

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            try
            {
                _logger.LogInformation($"Getting product with id: {id}");

                var result = await _productService.GetProduct(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                _logger.LogInformation("Adding new product");

                // Create the product and publish the event to the message broker
                await _productService.SendCreateEvent(productCreateDto);

                return Ok("Product Created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] ProductUpdateDto productUpdateDto, Guid id)
        {
            try
            {
                _logger.LogInformation($"Updating product with id: {id}");

                // Update the product and publish the event to the message broker
                await _productService.SendUpdateEvent(id, productUpdateDto);

                return Ok("Product Updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/checkstock")]
        public async Task<ActionResult> CheckStock([FromBody] List<ProductStockDto> productsFromOrder)
        {
            try
            {
                _logger.LogInformation($"Checking stock for products");

                var result = await _productService.CheckStock(productsFromOrder);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
