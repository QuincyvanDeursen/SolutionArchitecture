using OrderService.Domain;
using OrderService.Repository;
using OrderService.Services.Interface;
using Shared.Repository.Interface;

namespace OrderService.Services
{
    public class EventHandlerService(IWriteRepository<Payment> productWriteRepo) : IEventHandlerService
    {
        public async Task ProcessPaymentCreatedEvent(Payment payment)
        {
            await productWriteRepo.CreateAsync(payment);
        }

        public async Task ProcessProductUpdatedEvent(Payment payment)
        {
            await productWriteRepo.UpdateAsync(payment);
        }
    }
}
