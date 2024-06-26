﻿using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using Shared.Models;

namespace OrderService.Database
{
    public class OrderDbContext : DbContext
    {
        public DbSet<OrderPayment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderProducts { get; set; }
        public DbSet<OrderProduct> Products { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite primary key for OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId });

            // Configure the many-to-many relationship between Order and OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            // Configure the many-to-many relationship between Product and OrderItem
            modelBuilder.Entity<OrderProduct>()
                .HasMany(p => p.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.ProductId);

            
            // Seed data (orderproduct)
            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { Id = new Guid("f5f4b3b3-3b1b-4b7b-8b3b-3b1b4b7b8b3b"), Name = "Laptop", Price = 10 },
                new OrderProduct { Id = new Guid("f5f4b3b3-3b1b-4b7b-8b3b-3b1b4b7b8b3c"), Name = "Monitor", Price = 20 },
                new OrderProduct { Id = new Guid("f5f4b3b3-3b1b-4b7b-8b3b-3b1b4b7b8b3d"), Name = "Mouse", Price = 30 }
            );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
