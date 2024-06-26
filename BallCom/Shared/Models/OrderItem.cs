using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        
        // Related ids (also composite key)
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
