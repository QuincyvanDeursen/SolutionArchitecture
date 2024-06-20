using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.DTO;
using OrderService.Repository.Interface;
using OrderService.Services;
using OrderService.Services.Interface;
using System.Security.Cryptography;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        { 
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(Guid id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound(); 
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto order)
        {
            try
            {
                if (await _orderService.CreateOrder(order))
                {
                    return Ok("Order saved successfully");
                }
                else
                {
                    return BadRequest("One or more products are not in stock");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
