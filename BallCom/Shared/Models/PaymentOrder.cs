namespace Shared.Models;

public class PaymentOrder
{
    // Same Id as the order service
    public Guid Id { get; set;}
    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
    public OrderStatus Status { get; set; }
    
    // Related entities
    public Guid CustomerId { get; set; }
    
    public PaymentCustomer Customer { get; set; }
    
    public Payment Payment { get; set; }
}