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
            var order1 = Guid.NewGuid();
            var order2 = Guid.NewGuid();
            var order3 = Guid.NewGuid();
            IEnumerable<Order> orders = new List<Order>
            {
                new Order { Id = order1, OrderDate = DateTime.Now, CustomerId = 1, PaymentId = 1, Address = "Pelmolenstraat 11A, 4811LR, Breda" },
                new Order { Id = order2, OrderDate = DateTime.Now, CustomerId = 2, PaymentId = 2, Address = "Pelmolenstraat 11A, 4811LR, Breda" },
                new Order { Id = order3, OrderDate = DateTime.Now, CustomerId = 3, PaymentId = 3, Address = "Pelmolenstraat 11A, 4811LR, Breda" },
            };

            IEnumerable<OrderItem> orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), OrderId = order1, ProductId = Guid.NewGuid(), Quantity = 1 },
                new OrderItem { Id = Guid.NewGuid(), OrderId = order2, ProductId = Guid.NewGuid(), Quantity = 2 },
                new OrderItem { Id = Guid.NewGuid(), OrderId = order3, ProductId = Guid.NewGuid(), Quantity = 3 },
            };


            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().HasData(orders);

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>().HasData(orderItems);
        }

    }
}
