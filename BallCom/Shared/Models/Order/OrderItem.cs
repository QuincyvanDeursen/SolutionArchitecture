using System.Text.Json.Serialization;

namespace Shared.Models.Order
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        
        public decimal SnapshotPrice { get; set; }
        
        // Related ids (also composite key)
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        
        // Navigation properties
        public OrderProduct Product { get; set; }
        
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
