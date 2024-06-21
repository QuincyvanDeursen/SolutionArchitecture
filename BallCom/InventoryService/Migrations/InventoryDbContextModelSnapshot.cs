﻿// <auto-generated />
using System;
using InventoryService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryService.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ff7d052d-92e1-4a75-92e7-60691de7757d"),
                            Description = "HP Envy 16x",
                            Name = "Laptop",
                            Price = 799.0m,
                            Quantity = 10
                        },
                        new
                        {
                            Id = new Guid("3cc09248-d064-4bf8-bcb6-142283f0d185"),
                            Description = "Logitech MX Master",
                            Name = "Mouse",
                            Price = 40.0m,
                            Quantity = 0
                        },
                        new
                        {
                            Id = new Guid("f7db078e-66b8-4c86-9af3-a3c4e03f12e1"),
                            Description = "Coolermaster CK550",
                            Name = "Keyboard",
                            Price = 80.0m,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("InventoryService.Events.InventoryBaseEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("ProductJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InventoryEvents");

                    b.HasDiscriminator<string>("EventType").HasValue("InventoryBaseEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InventoryService.Events.InventoryCreatedEvent", b =>
                {
                    b.HasBaseType("InventoryService.Events.InventoryBaseEvent");

                    b.HasDiscriminator().HasValue("InventoryCreate");
                });

            modelBuilder.Entity("InventoryService.Events.InventoryUpdateEvent", b =>
                {
                    b.HasBaseType("InventoryService.Events.InventoryBaseEvent");

                    b.HasDiscriminator().HasValue("InventoryUpdate");
                });
#pragma warning restore 612, 618
        }
    }
}