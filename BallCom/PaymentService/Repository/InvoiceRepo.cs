using PaymentService.Database;
using PaymentService.Domain;
using PaymentService.Repository.Interfaces;

namespace PaymentService.Repository
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly PaymentDbContext _context;
        public InvoiceRepo(PaymentDbContext context)
        {
            _context = context;
        }
        public void DeleteInvoice(Invoice orderItem)
        {
             _context.Remove(orderItem);
        }

        public Invoice GetInvoice(int id)
        {
            return _context.Invoices.First(i => i.Id == id);
        }

        public IEnumerable<Invoice> GetInvoiceByCustomerId(int customerId)
        {
            return _context.Invoices.Where(i => i.CustomerId == customerId).ToList();
        }

        public IEnumerable<Invoice> GetInvoiceByOrderId(int orderId)
        {
            return _context.Invoices.Where(i => i.OrderId == orderId).ToList();
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices.ToList();
        }

        public void SaveInvoice(Invoice orderItem)
        {
            _context.Invoices.Add(orderItem);
        }

        public void UpdateInvoice(Invoice orderItem)
        {
            _context.Invoices.Update(orderItem);
        }
    }
}
