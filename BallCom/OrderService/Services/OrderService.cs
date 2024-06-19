using OrderService.Domain;
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

        public async Task<bool> CreateOrder(Order order)
        {
            if (IsOrderWithinProductLimit(order.OrderItems))
                if (await CheckStock(order))
                {
                    var result = await _orderRepo.SaveOrder(order);

                    foreach (var orderItem in order.OrderItems)
                    {
                        orderItem.OrderId = result.Id;
                        _orderItemRepo.SaveOrderItem(orderItem);
                    }
                    return true;
                }
            return false;
        }
        private async Task<bool> CheckStock(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _inventoryServiceClient.GetInventoryAsync(orderItem.ProductId);
                if (product.Quantity < orderItem.Quantity)
                {
                    throw new Exception($"Not enough inventory for product {orderItem.ProductId}");
                }
            }
            return true;
        }

        private bool IsOrderWithinProductLimit (ICollection<OrderItem> orderItems) 
        {
            if(orderItems.Count <= 20)
            {
                return true;
            }
            return false;
        }
    }
}
