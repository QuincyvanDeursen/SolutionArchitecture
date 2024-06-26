namespace Shared.Models.Order
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        
        public decimal SnapshotPrice { get; set; }
        
        // Related ids (also composite key)
        public Guid OrderId { get; set; }
        public Guid OrderProductId { get; set; }
    }
}
