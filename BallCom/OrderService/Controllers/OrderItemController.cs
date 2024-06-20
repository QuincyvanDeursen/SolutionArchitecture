﻿using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Repository.Interface;
using OrderService.Services.Interface;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IOrderItemRepo _orderItemRepo;


        public OrderItemController(ILogger<OrderItemController> logger, IOrderItemRepo orderItemRepo)
        {
            _logger = logger;
            _orderItemRepo = orderItemRepo;
        }

        [HttpGet]
        public async  Task<IEnumerable<OrderItem>> Get()
        {
            return await _orderItemRepo.GetOrderItems();
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> Get(Guid id)
        {
            var orderItem = _orderItemRepo.GetOrderItem(id);
            if (orderItem == null)
            {
                return NotFound(new { Message = $"OrderItem with ID {id} not found." });
            }
            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
        {
            var orderItems = _orderItemRepo.GetOrderItemsByOrderId(orderId);
            if (orderItems == null || !orderItems.Any())
            {
                return NotFound(new { Message = $"No OrderItems found for Order ID {orderId}." });
            }
            return Ok(orderItems);
        }

        [HttpPost]
        public void Post([FromBody] OrderItem orderItem)
        {
            _orderItemRepo.SaveOrderItem(orderItem);
        }

        [HttpPut]
        public void Put([FromBody] OrderItem orderItem)
        {
            _orderItemRepo.UpdateOrderItem(orderItem);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var item = await _orderItemRepo.GetOrderItem(id);
            _orderItemRepo.DeleteOrderItem(item);
        }
    }
}
