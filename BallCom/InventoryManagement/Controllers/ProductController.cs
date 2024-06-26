using System.Text.Json;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.CQRS.Queries;
using InventoryManagement.CQRS.Commands;
using InventoryManagement.CQRS.Commands.Handler;
using InventoryManagement.CQRS.Queries.Interfaces;
using Shared.Models;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductCommandHandler _commandHandler;
        private readonly IQueryHandler<GetProductQuery, Product> _getProductHandler;
        private readonly IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> _getAllProductsHandler;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductCommandHandler commandHandler, ILogger<ProductController> logger,
            IQueryHandler<GetProductQuery, Product> getProductHandler,
            IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> getAllProductsHandler)
        {
            _commandHandler = commandHandler;
            _logger = logger;
            _getProductHandler = getProductHandler;
            _getAllProductsHandler = getAllProductsHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _getAllProductsHandler.Handle(new GetAllProductsQuery());

                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling GetAllProducts query");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await _getProductHandler.Handle(new GetProductQuery(id));

                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling GetProduct query");
                return BadRequest();
            }
        }

        [HttpPost("checkstock")]
        public async Task<ActionResult> CheckStock([FromBody] List<CheckStock> input)
        {
            try
            {
                _logger.LogInformation($"Checking stock for products");

                var stock = true;

                foreach (var productItem in input)
                {
                    var product = await _getProductHandler.Handle(new GetProductQuery(productItem.ProductId));
                    Console.WriteLine(JsonSerializer.Serialize(product));
                    Console.WriteLine(JsonSerializer.Serialize(productItem));

                    if (product.Stock < productItem.Quantity)
                    {
                        stock = false;
                        break;
                    }
                }
                
                Console.WriteLine("Got here");

                return Ok(stock);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            try
            {
                await _commandHandler.Handle(command);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling CreateProduct command");
                return BadRequest();
            }
        }

        [HttpPost("increasestock")]
        public async Task<IActionResult> IncreaseStock(IncreaseStockCommand command)
        {
            try
            {
                await _commandHandler.Handle(command);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling IncreaseStock command");
                return BadRequest();
            }
        }

        [HttpPost("decreasestock")]
        public async Task<IActionResult> DecreaseStock(DecreaseStockCommand command)
        {
            try
            {
                await _commandHandler.Handle(command);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling DecreaseStock command");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            try
            {
                await _commandHandler.Handle(command);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while handling UpdateProduct command");
                return BadRequest();
            }
        }
    }
}