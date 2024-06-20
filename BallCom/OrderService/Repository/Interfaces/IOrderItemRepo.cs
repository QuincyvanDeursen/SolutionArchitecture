using OrderService.Domain;

namespace OrderService.Repository.Interface
{
    public interface IOrderItemRepo
    {
            IEnumerable<OrderItem> GetOrderItems();

            IEnumerable<OrderItem> GetOrderItemsByOrderId(Guid orderId);
            OrderItem GetOrderItem(Guid id);
            void SaveOrderItem(OrderItem orderItem);
            void UpdateOrderItem(OrderItem orderItem);
            void DeleteOrderItem(OrderItem orderItem);
    }
}
