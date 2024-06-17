using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Repository.Interface;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        //private readonly IMessagePublisher _messagePublisher;


        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService /*IMessagePublisher messagePublisher*/)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _inventoryService = inventoryService;
            //_messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                _logger.LogInformation("Getting all products");

                var products = await _inventoryService.GetAllProducts();

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

                var result = await _inventoryService.GetProduct(id);

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

                await _inventoryService.AddProduct(productCreateDto);

                return Ok();
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

                await _inventoryService.UpdateProduct(id, productUpdateDto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet("Test")]
        //public async Task Test()
        //{
        //    // Sending a message to the inventory.test topic (self listening)
        //    await _messagePublisher.PublishAsync(new { Message = "Hello World" }, "inventory.test");
        //}
    }
}
