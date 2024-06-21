using Shared.Models;

namespace PaymentService.Domain;

public class PaymentOrder
{
    // Same Id as the order service
    public Guid Id { get; set;}
    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
    public OrderStatus Status { get; set; }
    
    // Related entities
    public Guid CustomerId { get; set; }
}