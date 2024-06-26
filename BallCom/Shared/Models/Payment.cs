using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Payment
    {
        public Guid Id { get; init; }
        public decimal TotalPrice { get; set; }
        public PaymentStatus Status { get; set; }
        
        public Guid OrderId { get; init; }
        public Guid CustomerId { get; init; }
        
        
        // Payment related entities (eventual consistency)
        // Can be ignored when serializing as this is specific to payments
        [JsonIgnore]
        public PaymentOrder Order { get; set; }  // Navigation property
        
        [JsonIgnore]
        public PaymentCustomer Customer { get; set; }  // Navigation property
    }
}
