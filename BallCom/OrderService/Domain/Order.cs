using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentId { get; set; } 
        public string? Address {  get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public decimal Totalprice { get; set; } = 0;
    }
}
