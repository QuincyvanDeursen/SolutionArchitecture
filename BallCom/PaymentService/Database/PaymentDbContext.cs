using Microsoft.EntityFrameworkCore;
using PaymentService.Domain;
using Shared.Models;


namespace PaymentService.Database
{
    public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentOrder> Orders { get; set; }
        public DbSet<PaymentCustomer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Payment> payments = new List<Payment>
            {
                new Payment { Id = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), TotalPrice = 100.00m, Status = PaymentStatus.Paid },
                new Payment { Id = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), TotalPrice = 200.00m, Status = PaymentStatus.Paid },
                new Payment { Id = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), TotalPrice = 300.00m, Status = PaymentStatus.Pending}
            };

            modelBuilder.Entity<Payment>().HasKey(i => i.Id);
            modelBuilder.Entity<Payment>().HasData(payments);
        }
    }
}
