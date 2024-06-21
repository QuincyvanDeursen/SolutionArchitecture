using Shared.Models;

namespace PaymentService.Dto;

public class PaymentUpdateDto
{
    public PaymentStatus Status { get; set; }
}