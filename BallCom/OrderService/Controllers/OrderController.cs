using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Repository.Interface;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepo _orderRepo; // Mogelijk moet dit de repo service worden ipv db.


        public OrderController(ILogger<OrderController> logger, IOrderRepo orderRepo) { 
        
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


    }
}
