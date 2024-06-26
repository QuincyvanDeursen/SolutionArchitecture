using System.Text.Json.Serialization;

namespace Shared.Models.Order;

public class OrderProduct
{
    public Guid Id { get; init; }
    public String Name { get; set; }
    public decimal Price { get; set; }
    
    // Navigation properties 
    // This property is not serialized to avoid circular reference
    [JsonIgnore]
    public ICollection<OrderItem> OrderItems { get; set; }
}