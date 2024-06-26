using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Models.Payment;


namespace PaymentService.Database
{
    public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentOrder> Orders { get; set; }
        public DbSet<PaymentCustomer> Customers { get; set; }
    }
}
