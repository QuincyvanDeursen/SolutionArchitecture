using System.ComponentModel.DataAnnotations;

namespace OrderService.DTO
{
    public class OrderItemCreateDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
