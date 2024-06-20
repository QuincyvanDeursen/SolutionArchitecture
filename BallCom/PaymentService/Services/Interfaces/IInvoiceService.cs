using PaymentService.Dto;

namespace PaymentService.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<bool> CreateInvoice(InvoiceCreateDto invoiceCreateDto);
    }
}
