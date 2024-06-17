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
        private readonly IOrderRepo _orderRepo;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IOrderRepo orderRepo)
        { 
            _orderRepo = orderRepo;
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> Get()
        {
            return await _orderRepo.GetAllOrdersAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orderRepo.GetOrder(id);

            if (order == null)
            {
                return NotFound(); 
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
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

        [HttpPut]
        public void Put([FromBody] Order order)
        {
            _orderRepo.UpdateOrder(order);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _orderRepo.DeleteOrder(_orderRepo.GetOrder(id));
        }
    }
}
