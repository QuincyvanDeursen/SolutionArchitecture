using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dto
{
    public class InvoiceCreateDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
    }
}
