using OrderService.Domain;
using OrderService.DTO;
using OrderService.EventHandlers.Interfaces;
using OrderService.Events;
using OrderService.Repository.Interface;
using OrderService.Services.Interface;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IInventoryServiceClient _inventoryServiceClient;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderEventHandler _orderEventHandler;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IInventoryServiceClient inventoryServiceClient, IOrderRepo orderRepo, IOrderEventHandler orderEventHandler, ILogger<OrderService> logger)
        {
            _inventoryServiceClient = inventoryServiceClient;
            _orderRepo = orderRepo;
            _orderEventHandler = orderEventHandler;
            _logger = logger;
        }

        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            if (IsOrderWithinProductLimit(orderCreateDto.OrderItems) && await CheckStock(orderCreateDto.OrderItems))
            {
                var Id = Guid.NewGuid();
                var orderItems = ConvertToOrderItem(orderCreateDto.OrderItems, Id);
                var order = new Order
                {
                    Id = Id,
                    OrderDate = DateTime.Now,
                    CustomerId = orderCreateDto.CustomerId,
                    Address = orderCreateDto.Address,
                    OrderItems = orderItems,
                    Totalprice = orderItems.Sum(i => i.ProductPrice * i.Quantity)
                };

                //Order opslaan
                await _orderRepo.CreateOrder(order);

                var OrderCreatedEvent = new OrderCreatedEvent(order);
                await _orderEventHandler.Handle(OrderCreatedEvent);
            } else
            {
                _logger.LogError("One or more products are not in stock");
                throw new Exception("One or more products are not in stock");
            }
        }
        private async Task<bool> CheckStock(ICollection<OrderItemCreateDto> orderItems)
        {
            var products = new List<ProductStockDto>();
            foreach (var item in orderItems)
            {
                ProductStockDto product = new ProductStockDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                products.Add(product);
            }
            return await _inventoryServiceClient.CheckStockAsync(products);
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
