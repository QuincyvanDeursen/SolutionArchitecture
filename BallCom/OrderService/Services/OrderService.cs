using OrderService.DTO;
using OrderService.Services.Interface;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Models.Order;
using Shared.Repository.Interface;

namespace OrderService.Services
{
    public class OrderService(
        IInventoryServiceClient inventoryServiceClient,
        IWriteRepository<Order> orderWriteRepository,
        IReadRepository<Order> orderReadRepository,
        IReadRepository<OrderProduct> orderProductReadRepository,
        IMessagePublisher messagePublisher
        ) : IOrderService
    {
        public async Task<Order> GetOrderById(Guid id)
        {
            // 1. Get order by id from the repository and return it
            return await orderReadRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            // 1. Get all orders from the repository and return them
            return await orderReadRepository.GetAllAsync();
        }
        
        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            // 1. Check if the order contains more than 20 items
            if (orderCreateDto.OrderItems.Count > 20)
                throw new ArgumentException("You can only order a maximum of 20 items at a time");
            
            // 2. Check if all products are in stock
            // TODO: Re-enable this check after fixing the inventory service
            // if (!await inventoryServiceClient.CheckStockAsync(orderCreateDto.OrderItems))
            //     throw new Exception("One or more products are not in stock");
            
            // 3. Check if all order items exist in the database
            if (!await AllOrderProductsExist(orderCreateDto.OrderItems))
                throw new KeyNotFoundException("One or more products in the order do not exist");
            
            // 3. Convert DTO to domain model
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderDate = DateTime.Now,
                CustomerId = orderCreateDto.CustomerId,
                Address = orderCreateDto.Address,
                OrderItems = orderCreateDto.OrderItems.Select(oi => new OrderItem
                {
                    OrderId = orderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                }).ToList()
            };
            
            order.OrderItems.ToList().ForEach(async i =>
            {
                i.SnapshotPrice = await GetSnapshotPrice(i.ProductId);
            });
            
            order.TotalPrice = await GetTotalOrderPrice(order.OrderItems);
            
            
            // 4. Save order to database
            await orderWriteRepository.CreateAsync(order);
            
            // 5. Send a message that a new order has been placed
            await messagePublisher.PublishAsync(order, "order.create");
        }

        public async Task UpdateOrder(Guid id, OrderUpdateDto newOrder)
        {
            // 1. Check if the order exists
            var existingOrder = await orderReadRepository.GetByIdAsync(id);
            if (existingOrder == null)
                throw new KeyNotFoundException("Order was not found");
            
            // 2. Update only updateable properties
            existingOrder.Address = newOrder.Address;
            
            // 3. Save the updated order to the database
            await orderWriteRepository.UpdateAsync(existingOrder);
            
            // 4. Send a message that the order has been updated
            await messagePublisher.PublishAsync(existingOrder, "order.update");
        }

        public async Task UpdateOrderStatus(Guid id, OrderUpdateStatusDto newOrder)
        {
            // 1. Check if the order exists
            var existingOrder = await orderReadRepository.GetByIdAsync(id);
            if (existingOrder == null)
                throw new KeyNotFoundException("Order was not found");
            
            // 2. Check if the new status is valid (only "Shipped", "Delivered" and "Failed" are allowed)
            if (newOrder.Status != OrderStatus.Shipped && newOrder.Status != OrderStatus.Delivered &&
                newOrder.Status != OrderStatus.Failed)
            {
                throw new ArgumentException($"Changing from {existingOrder.Status} to status {newOrder.Status} is not allowed directly");
            }
            
            // 3. Update only the status
            existingOrder.Status = newOrder.Status;
            
            // 3. Save the updated order to the database
            await orderWriteRepository.UpdateAsync(existingOrder);
            
            // 4. Send a message that the order has been updated
            await messagePublisher.PublishAsync(existingOrder, "order.update");
        }

        private async Task<bool> AllOrderProductsExist(ICollection<OrderItemDto> orderItems)
        {
            foreach (var item in orderItems)
            {
                var product = await orderProductReadRepository.GetByIdAsync(item.ProductId);
                if (product == null) return false;
            }
            return true;
        }

        private async Task<decimal> GetTotalOrderPrice(ICollection<OrderItem> orderItems)
        {
            decimal totalPrice = 0;
            foreach (var item in orderItems)
            {
                var product = await orderProductReadRepository.GetByIdAsync(item.ProductId);
                totalPrice += product.Price * item.Quantity;
            }
            return totalPrice;
        }
        
        private async Task<decimal> GetSnapshotPrice(Guid productId)
        {
            var product = await orderProductReadRepository.GetByIdAsync(productId);
            return product.Price;
        }
    }
}
