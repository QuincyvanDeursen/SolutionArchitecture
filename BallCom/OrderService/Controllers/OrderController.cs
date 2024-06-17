using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Repository.Interface;
using Shared.MessageBroker.Publisher.Interfaces;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepo _orderRepo; // Mogelijk moet dit de repo service worden ipv db.
        private readonly IMessagePublisher _messagePublisher;
        
        public OrderController(ILogger<OrderController> logger, IOrderRepo orderRepo, IMessagePublisher messagePublisher) { 
            _messagePublisher = messagePublisher;
            _logger = logger;
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderRepo.GetOrders();
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
        public void Post([FromBody] Order order)
        {
            _orderRepo.SaveOrder(order);
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

        [HttpGet("Test")]
        public async Task Test()
        {
            await _messagePublisher.PublishAsync(new { Message = "Hello World"}, "order.test");
        }
    }
}
