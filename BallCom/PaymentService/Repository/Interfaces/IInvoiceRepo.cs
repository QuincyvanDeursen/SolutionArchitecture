using PaymentService.Domain;

namespace PaymentService.Repository.Interfaces
{
    public interface IInvoiceRepo
    {
        IEnumerable<Invoice> GetInvoices();

        IEnumerable<Invoice> GetInvoiceByOrderId(int orderId);

        IEnumerable<Invoice> GetInvoiceByCustomerId(int customerId);
        Invoice GetInvoice(int id);
        void SaveInvoice(Invoice orderItem);
        void UpdateInvoice(Invoice orderItem);
        void DeleteInvoice(Invoice orderItem);
    }
}
