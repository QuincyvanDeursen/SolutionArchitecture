using System.Text.Json.Serialization;

namespace Shared.Models.Payment
{
    public class Payment
    {
        public Guid Id { get; init; }
        public decimal TotalPrice { get; set; }
        public PaymentStatus Status { get; set; }
        
        public Guid OrderId { get; init; }
        public Guid CustomerId { get; init; }
        
        
        // Payment related entities (eventual consistency)
        public PaymentOrder Order { get; set; } 
        public PaymentCustomer Customer { get; set; }
    }
}
