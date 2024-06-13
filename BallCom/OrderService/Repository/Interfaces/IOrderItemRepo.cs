using OrderService.Domain;

namespace OrderService.Repository.Interface
{
    public interface IOrderItemRepo
    {
            IEnumerable<OrderItem> GetOrderItems();

            IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
            OrderItem GetOrderItem(int id);
            void SaveOrderItem(OrderItem orderItem);
            void UpdateOrderItem(OrderItem orderItem);
            void DeleteOrderItem(OrderItem orderItem);
    }
}
