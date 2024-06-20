using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Database
{
    public class OrderDbContext : DbContext
    {

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
