using PaymentService.Dto;
using PaymentService.Services.Interfaces;
using Shared.MessageBroker.Publisher.Interfaces;
using Shared.Models;
using Shared.Repository.Interface;

namespace PaymentService.Services;

public class PaymentService(
    IWriteRepository<Payment> paymentWriteRepo, 
    IReadRepository<Payment> paymentReadRepo,
    IMessagePublisher messagePublisher
) : IPaymentService
{
    public async Task<IEnumerable<Payment>> GetAll()
    {
        return await paymentReadRepo.GetAllAsync();
    }

    public async Task<Payment> Get(Guid id)
    {
        return await paymentReadRepo.GetByIdAsync(id);
    }

    public async Task Update(Guid id, PaymentUpdateDto payment)
    {
        // 1. Retrieve the old payment and update the status
        var oldPayment = await paymentReadRepo.GetByIdAsync(id);
        if(oldPayment == null) throw new KeyNotFoundException($"Payment with ID {id} not found.");

        // 1.1 Update status (pending -> paid | cancelled)
        if(oldPayment.Status is PaymentStatus.Paid or PaymentStatus.Cancelled)
            throw new InvalidOperationException("Payment status cannot be updated after it has been paid or cancelled.");
        
        oldPayment.Status = payment.Status;
        
        // 1.2 Update the payment in the database
        await paymentWriteRepo.UpdateAsync(oldPayment);
        
        // 2. Publish an event to notify other services
        await messagePublisher.PublishAsync(oldPayment, "payment.update");
    }
}