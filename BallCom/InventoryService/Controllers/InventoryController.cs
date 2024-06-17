using InventoryService.Domain;
using InventoryService.Dto;
using InventoryService.EventHandlers.Interfaces;
using InventoryService.Events;
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
        private readonly IReadRepository<Product> _productReadRepo;
        private readonly IInventoryEventHandler _inventoryEventHandler;
        //private readonly IMessagePublisher _messagePublisher;


        public InventoryController(ILogger<InventoryController> logger, IReadRepository<Product> productReadRepo, IInventoryEventHandler inventoryEventHandler /*IMessagePublisher messagePublisher*/)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productReadRepo = productReadRepo ?? throw new ArgumentNullException(nameof(productReadRepo));
            _inventoryEventHandler = inventoryEventHandler ?? throw new ArgumentNullException(nameof(inventoryEventHandler));
            //_messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productReadRepo.GetAllAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var result = await _productReadRepo.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductCreateDto productCreateDto)
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

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] ProductUpdateDto productUpdateDto, Guid id)
        {
            var oldProduct = await _productReadRepo.GetByIdAsync(id);
            if (oldProduct == null)
            {
                return NotFound();
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

            return Ok(); 
        }

        //[HttpGet("Test")]
        //public async Task Test()
        //{
        //    // Sending a message to the inventory.test topic (self listening)
        //    await _messagePublisher.PublishAsync(new { Message = "Hello World" }, "inventory.test");
        //}
    }
}
