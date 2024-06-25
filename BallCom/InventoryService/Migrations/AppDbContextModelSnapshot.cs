// <auto-generated />
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
    partial class AppDbContextModelSnapshot : ModelSnapshot
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
                            Id = new Guid("e53ece0f-74e5-48e9-b346-c1939dacf846"),
                            Description = "HP Envy 16x",
                            Name = "Laptop",
                            Price = 799.0m,
                            Quantity = 10
                        },
                        new
                        {
                            Id = new Guid("2b81379b-7034-4e29-bb9f-83ea196e8b08"),
                            Description = "Logitech MX Master",
                            Name = "Mouse",
                            Price = 40.0m,
                            Quantity = 0
                        },
                        new
                        {
                            Id = new Guid("38ed5083-fcdb-497d-acba-73352e24415c"),
                            Description = "Coolermaster CK550",
                            Name = "Keyboard",
                            Price = 80.0m,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("InventoryService.Events.ProductBaseEvent", b =>
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

                    b.ToTable("ProductEvents");

                    b.HasDiscriminator<string>("EventType").HasValue("ProductBaseEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InventoryService.Events.ProductCreateEvent", b =>
                {
                    b.HasBaseType("InventoryService.Events.ProductBaseEvent");

                    b.HasDiscriminator().HasValue("InventoryCreate");
                });

            modelBuilder.Entity("InventoryService.Events.ProductUpdateEvent", b =>
                {
                    b.HasBaseType("InventoryService.Events.ProductBaseEvent");

                    b.HasDiscriminator().HasValue("InventoryUpdate");
                });
#pragma warning restore 612, 618
        }
    }
}
