using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Order
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; init; }
        public string? Address {  get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public OrderStatus Status { get; set; } = OrderStatus.Placed;
        
        // Related entities
        public Guid CustomerId { get; init; }
        public Guid? PaymentId { get; set; } 
        public ICollection<OrderItem> OrderItems { get; init; }
    }
}
