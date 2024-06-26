namespace Shared.Models.Payment;

public class PaymentCustomer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}