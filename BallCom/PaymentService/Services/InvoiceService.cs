using Microsoft.AspNetCore.Http.HttpResults;
using PaymentService.Domain;
using PaymentService.Dto;
using PaymentService.Services.Interfaces;

namespace PaymentService.Services
{
    public class InvoiceService : IInvoiceService
    {
        public async Task<bool> CreateInvoice(InvoiceCreateDto invoiceCreateDto)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                OrderId = invoiceCreateDto.OrderId,
                CustomerId = invoiceCreateDto.CustomerId,
                TotalAmount = invoiceCreateDto.TotalAmount,
                Status = InvoiceStatus.Pending
            };
            //await _eventWriteRepo.CreateAsync(inventoryEvent);
            return true;
        }
    }
}
