using Microsoft.EntityFrameworkCore;
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
            // Configure a one-to-many relationship between PaymentCustomer and Payment
            modelBuilder.Entity<PaymentCustomer>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);
            
            // Configure a one-to-one relationship between Payment and PaymentOrder
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<PaymentOrder>(o => o.Id);
        }
    }
}
