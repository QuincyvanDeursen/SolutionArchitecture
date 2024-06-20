namespace PaymentService.Domain
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
