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
        
        // Ef core navigation properties
        [JsonIgnore]
        public PaymentCustomer Customer { get; set; }
        
        [JsonIgnore]
        public PaymentOrder Order { get; set; }
    }
}
