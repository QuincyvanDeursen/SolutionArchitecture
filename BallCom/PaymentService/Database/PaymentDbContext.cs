using Microsoft.EntityFrameworkCore;
using PaymentService.Domain;

namespace PaymentService.Database
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Invoice> invoices = new List<Invoice>
            {
                new Invoice { Id = Guid.NewGuid(), OrderId = 1, CustomerId = 1, TotalAmount = 100.00m, Status = InvoiceStatus.Paid },
                new Invoice { Id = Guid.NewGuid(), OrderId = 2, CustomerId = 2, TotalAmount = 200.00m, Status = InvoiceStatus.Paid },
                new Invoice { Id = Guid.NewGuid(), OrderId = 3, CustomerId = 3, TotalAmount = 300.00m, Status = InvoiceStatus.Pending}
            };

            modelBuilder.Entity<Invoice>().HasKey(i => i.Id);
            modelBuilder.Entity<Invoice>().HasData(invoices);
        }
    }
}
