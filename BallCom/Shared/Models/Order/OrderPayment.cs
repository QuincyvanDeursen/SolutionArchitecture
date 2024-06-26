using Shared.Models.Payment;

namespace Shared.Models.Order;

public class OrderPayment
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public PaymentStatus Status { get; set; }
}