using OrderService.Domain;
using OrderService.DTO;
using OrderService.EventHandlers.Interfaces;
using OrderService.Events;
using OrderService.Repository.Interface;
using OrderService.Services.Interface;
using System.Text.Json;

namespace OrderService.Services.RabbitMQ
{
    public class OrderService : IOrderService
    {
        private readonly IInventoryServiceClient _inventoryServiceClient;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderEventHandler _orderEventHandler;


        public OrderService(IInventoryServiceClient inventoryServiceClient, IOrderRepo orderRepo, IOrderEventHandler orderEventHandler)
        {
            _inventoryServiceClient = inventoryServiceClient;
            _orderRepo = orderRepo;
            _orderEventHandler = orderEventHandler;
        }

        public async Task<bool> CreateOrder(OrderCreateDto orderCreateDto)
        {
            if (IsOrderWithinProductLimit(orderCreateDto.OrderItems) && await CheckStock(orderCreateDto.OrderItems))
            {
                var OrderId = Guid.NewGuid();
                var orderItems = ConvertToOrderItem(orderCreateDto.OrderItems, OrderId);
                var order = new Order
                {
                    Id = OrderId,
                    OrderDate = DateTime.Now,
                    CustomerId = orderCreateDto.CustomerId,
                    Address = orderCreateDto.Address,
                    OrderItems = orderItems,
                    Totalprice = calculateTotalPrice(orderItems)
                };

                //Order opslaan
                await _orderRepo.SaveOrder(order);

                //Order created event
                OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent(order);
                await _orderEventHandler.Handle(orderCreatedEvent);

                return true;
            }
            return false;
        }
        private async Task<bool> CheckStock(ICollection<OrderItemCreateDto> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var product = await _inventoryServiceClient.GetInventoryAsync(orderItem.ProductId);
                if (product.Quantity < orderItem.Quantity)
                {
                    throw new Exception($"Not enough inventory for product {orderItem.ProductId}");
                }
            }
            return true;
        }

        private bool IsOrderWithinProductLimit(ICollection<OrderItemCreateDto> orderItems)
        {
            if (orderItems.Count <= 20)
            {
                return true;
            }
            return false;
        }

        private ICollection<OrderItem> ConvertToOrderItem(ICollection<OrderItemCreateDto> orderItemCreateDtos, Guid id)
        {
            var orderItems = new List<OrderItem>();
            foreach (var item in orderItemCreateDtos)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = id,
                    ProductName = item.ProductName,
                    ProductId = item.ProductId,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }
            return orderItems;
        }

        private decimal calculateTotalPrice(ICollection<OrderItem> orderItems)
        {
            decimal totalPrice = 0;
            foreach (var item in orderItems)
            {
                totalPrice += item.ProductPrice;
            }
            return totalPrice;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _orderRepo.GetOrder(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderRepo.GetAllOrdersAsync();
        }
    }
}
