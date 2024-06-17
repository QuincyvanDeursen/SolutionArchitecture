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

        private async Task<bool> ProductsInStockAsync(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var inventory = await _inventoryServiceClient.GetInventoryAsync(orderItem.ProductId);
                if (inventory.Quantity < orderItem.Quantity)
                {
                    throw new Exception($"Not enough inventory for product {orderItem.ProductId}");
                }
            }
            return true;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            // Check if products are in stock
            bool inStock = await ProductsInStockAsync(order);
            if (inStock)
            {
                // Save the order
                var result = await _orderRepo.SaveOrder(order);

                foreach(var orderItem in order.OrderItems)
                {
                    orderItem.OrderId = result.Id;
                    _orderItemRepo.SaveOrderItem(orderItem);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
