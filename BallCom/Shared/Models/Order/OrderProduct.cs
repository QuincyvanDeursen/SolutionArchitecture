namespace Shared.Models.Order;

public class OrderProduct
{
    public Guid Id { get; init; }
    public String Name { get; set; }
    public decimal Price { get; set; }
    
    // Navigation properties 
    public ICollection<OrderItem>? OrderItems { get; set; }
}