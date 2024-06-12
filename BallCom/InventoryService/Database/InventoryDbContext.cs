using InventoryService.Domain;
using InventoryService.Events;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Database
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<InventoryEvent> InventoryEvents { get; set; }


        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Description = "HP Envy 16x", Price = 799.0m },
                new Product { Id = 2, Name = "Mouse", Description = "Logitech MX Master", Price = 40.0m },
                new Product { Id = 3, Name = "Keyboard", Description = "Coolermaster CK550", Price = 80.0m }
            };

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasData(products);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InventoryEvent>().ToTable("InventoryEvents");

            modelBuilder.Entity<InventoryEvent>()
                .Property(e => e.EventType)
                .HasConversion<int>();
        }
    }
}
