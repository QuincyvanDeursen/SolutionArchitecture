using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderService.Domain
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        // is purely a reference to the Order object for entity framework
        // JsonIgnore is used to prevent circular reference when serializing the object
        [JsonIgnore]
        public Order Order { get; set; } 
    }
}
