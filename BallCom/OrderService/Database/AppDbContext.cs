using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
