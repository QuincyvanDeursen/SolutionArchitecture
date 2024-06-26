using InventoryManagement.Domain;
using InventoryManagement.Events;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Event> ProductEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired();

            //seed data for product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 10,
                    Stock = 10
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description 2",
                    Price = 20,
                    Stock = 20
                }
            );

            modelBuilder.Entity<Event>().Ignore(e => e.Product);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EventType).IsRequired();

                entity.HasDiscriminator<string>("EventType")
                    .HasValue<ProductCreatedEvent>("ProductCreated")
                    .HasValue<StockIncreasedEvent>("StockIncreased");
            });

            // Explicitly register the entities if necessary
            modelBuilder.Entity<ProductCreatedEvent>();
            modelBuilder.Entity<StockIncreasedEvent>();
        }
    }
}
