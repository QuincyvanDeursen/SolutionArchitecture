using System.Text.Json.Serialization;

namespace Shared.Models.Order
{
    public class Order
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; init; }
        public string? Address {  get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public OrderStatus Status { get; set; } = OrderStatus.Placed;
        public Guid CustomerId { get; init; }
        public Guid? PaymentId { get; set; } 
        
        
        // Payment related entities (eventual consistency)
        public ICollection<OrderItem> OrderItems { get; init; }
        public OrderPayment? Payment { get; set; }
        public OrderCustomer Customer { get; set; }
    }
}
