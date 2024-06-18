using InventoryService.Domain;
using InventoryService.Events;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Database
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryBaseEvent> InventoryEvents { get; set; }


        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "HP Envy 16x", Price = 799.0m, Quantity = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Mouse", Description = "Logitech MX Master", Price = 40.0m, Quantity = 0},
                new Product { Id = Guid.NewGuid(), Name = "Keyboard", Description = "Coolermaster CK550", Price = 80.0m, Quantity = 1}
            };

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasData(products);

            modelBuilder.Entity<InventoryBaseEvent>().Ignore(p => p.Product);

            modelBuilder.Entity<InventoryBaseEvent>().HasKey(ie => ie.Id);
            modelBuilder.Entity<InventoryBaseEvent>()
                .HasDiscriminator<string>("EventType")
                .HasValue<InventoryCreatedEvent>("InventoryCreate")
                .HasValue<InventoryUpdateEvent>("InventoryUpdate");
        }
    }
}
