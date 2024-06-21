using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.DTO;
using OrderService.Services.Interface;

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
                return BadRequest("Error processing order, certain items might be out of stock");
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
        public async Task<ActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatusUpdateDto order)
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
