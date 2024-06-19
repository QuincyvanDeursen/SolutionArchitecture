using OrderService.Domain;
using OrderService.DTO;
using OrderService.Repository.Interface;
using OrderService.Services.Interface;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IInventoryServiceClient _inventoryServiceClient;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderItemRepo _orderItemRepo;

        public OrderService(IInventoryServiceClient inventoryServiceClient, IOrderRepo orderRepo, IOrderItemRepo orderItemRepo)
        {
            _inventoryServiceClient = inventoryServiceClient;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        public async Task<bool> CreateOrder(OrderCreateDto orderCreateDto)
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
                    };
                    
                    //Order opslaan
                    await _orderRepo.SaveOrder(order);
                    
                    //Orderitems opslaan
                    foreach (var orderItem in order.OrderItems)
                    {
                        _orderItemRepo.SaveOrderItem(orderItem);
                    }
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

        private bool IsOrderWithinProductLimit (ICollection<OrderItemCreateDto> orderItems) 
        {
            if(orderItems.Count <= 20)
            {
                return true;
            }
            return false;
        }

        private ICollection<OrderItem> ConvertToOrderItem(ICollection<OrderItemCreateDto> orderItemCreateDtos, Guid id)
        {
            var orderItems = new List<OrderItem>();
            foreach(var item in orderItemCreateDtos)
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
