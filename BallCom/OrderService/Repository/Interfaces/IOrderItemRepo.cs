using OrderService.Domain;

namespace OrderService.Repository.Interface
{
    public interface IOrderItemRepo
    {
            Task <IEnumerable<OrderItem>> GetOrderItems();
            IEnumerable<OrderItem> GetOrderItemsByOrderId(Guid orderId);
            Task <OrderItem> GetOrderItem(Guid id);
            Task<OrderItem> SaveOrderItem(OrderItem orderItem);
            void UpdateOrderItem(OrderItem orderItem);
            void DeleteOrderItem(OrderItem orderItem);
    }
}
