using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Database
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Inventory>? Inventories { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Description = "Product1 Description", Price = 10.0m },
                new Product { Id = 2, Name = "Product2", Description = "Product2 Description", Price = 20.0m },
                new Product { Id = 3, Name = "Product3", Description = "Product3 Description", Price = 30.0m }
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

            modelBuilder.Entity<Inventory>()
                .HasOne<Product>()
                .WithOne()
                .HasForeignKey<Inventory>(i => i.ProductId);
        }
    }
}
