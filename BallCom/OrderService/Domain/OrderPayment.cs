using Shared.Models;

namespace OrderService.Domain;

public class OrderPayment
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public PaymentStatus Status { get; set; }
}