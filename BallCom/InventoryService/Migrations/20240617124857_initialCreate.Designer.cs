﻿// <auto-generated />
using System;
using InventoryService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryService.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20240617124857_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("20958433-57e6-405b-8373-e00f45198850"),
                            Description = "HP Envy 16x",
                            Name = "Laptop",
                            Price = 799.0m,
                            Quantity = 10
                        },
                        new
                        {
                            Id = new Guid("e18f3437-9537-4cc4-85a2-cf94e5d6f626"),
                            Description = "Logitech MX Master",
                            Name = "Mouse",
                            Price = 40.0m,
                            Quantity = 0
                        },
                        new
                        {
                            Id = new Guid("c60027eb-b622-4aa0-9096-6b285872aa30"),
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

                    b.Property<string>("Product")
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

                    b.HasDiscriminator().HasValue("InventoryCreated");
                });

            modelBuilder.Entity("InventoryService.Events.InventoryRemoveEvent", b =>
                {
                    b.HasBaseType("InventoryService.Events.InventoryBaseEvent");

                    b.HasDiscriminator().HasValue("InventoryRemoved");
                });
#pragma warning restore 612, 618
        }
    }
}