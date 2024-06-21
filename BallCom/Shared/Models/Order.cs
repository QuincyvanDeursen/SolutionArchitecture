using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Order
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; init; }
        public string? Address {  get; init; }
        public decimal TotalPrice { get; init; } = 0;
        public OrderStatus Status { get; set; } = OrderStatus.Placed;
        
        // Related entities
        public Guid CustomerId { get; init; }
        public Customer? Customer { get; init; }
        public Guid? PaymentId { get; init; } 
        public Payment? Payment { get; init; }
        public ICollection<OrderItem>? OrderItems { get; init; }
    }
}
