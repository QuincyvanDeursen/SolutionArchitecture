using PaymentService.Dto;
using Shared.Models;

namespace PaymentService.Services.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> GetAll();
    Task<Payment> Get(Guid id);
    Task Update(Guid id, PaymentUpdateDto payment);
}