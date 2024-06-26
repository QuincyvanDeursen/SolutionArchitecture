using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? PaymentId { get; set; }
        public string? Address { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public decimal Totalprice { get; set; } = 0;
        public OrderStatus OrderStatus { get; set; }
    }
}
