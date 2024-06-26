using Shared.Models;
using Shared.Models.Payment;

namespace PaymentService.Dto;

public class PaymentUpdateDto
{
    public PaymentStatus Status { get; set; }
}