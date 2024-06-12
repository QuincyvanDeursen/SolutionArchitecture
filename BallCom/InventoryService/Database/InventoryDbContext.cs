using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Database
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
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

            IEnumerable<Inventory> inventories = new List<Inventory>
            {
                new Inventory { Id = 1, ProductId = 1, Quantity = 10 },
                new Inventory { Id = 2, ProductId = 2, Quantity = 20 },
                new Inventory { Id = 3, ProductId = 3, Quantity = 30 }
            };

            modelBuilder.Entity<Inventory>().HasKey(i => i.Id);
            modelBuilder.Entity<Inventory>().HasData(inventories);

            modelBuilder.Entity<InventoryEvent>().HasKey(ie => ie.Id);

            modelBuilder.Entity<Inventory>()
                .HasOne<Product>()
                .WithOne()
                .HasForeignKey<Inventory>(i => i.ProductId);
        }
    }
}
