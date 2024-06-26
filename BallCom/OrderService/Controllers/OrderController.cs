using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Services.Interface;
using Shared.Models;
using Shared.Models.Order;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _orderService.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading order list");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null) return NotFound(); 

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading single order with id: {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderCreateDto order)
        {
            try
            {
                await _orderService.CreateOrder(order);
                return Ok("Order saved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] OrderUpdateDto order)
        {
            try
            {
                await _orderService.UpdateOrder(id, order);
                return Ok("Order updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order");
                return BadRequest("Error updating order");
            }
        }
        
        
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderUpdateStatusDto order)
        {
            try
            {
                await _orderService.UpdateOrderStatus(id, order);
                return Ok("Order updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order");
                return BadRequest("Error updating order");
            }
        }
    }
}
