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
            IEnumerable<Order> orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = DateTime.Now, CustomerId = 1, PaymentId = 1 },
                new Order { Id = 2, OrderDate = DateTime.Now, CustomerId = 2, PaymentId = 2 },
                new Order { Id = 3, OrderDate = DateTime.Now, CustomerId = 3, PaymentId = 3 },
            };

            IEnumerable<OrderItem> orderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                new OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 2 },
                new OrderItem { Id = 3, OrderId = 3, ProductId = 3, Quantity = 3 },
            };


            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().HasData(orders);

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>().HasData(orderItems);

            modelBuilder.Entity<Order>()
                .HasMany<OrderItem>(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

        }

    }
}
