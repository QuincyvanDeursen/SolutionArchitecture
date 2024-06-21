﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
<<<<<<<< HEAD:BallCom/InventoryService/Migrations/20240620150824_initialCreate.cs
                    { new Guid("2b81379b-7034-4e29-bb9f-83ea196e8b08"), "Logitech MX Master", "Mouse", 40.0m, 0 },
                    { new Guid("38ed5083-fcdb-497d-acba-73352e24415c"), "Coolermaster CK550", "Keyboard", 80.0m, 1 },
                    { new Guid("e53ece0f-74e5-48e9-b346-c1939dacf846"), "HP Envy 16x", "Laptop", 799.0m, 10 }
========
                    { new Guid("08f4e435-02f1-44b9-8164-f81c94abe821"), "Logitech MX Master", "Mouse", 40.0m, 0 },
                    { new Guid("63e69ab1-ca66-4c28-aaaa-aa65dd03b172"), "HP Envy 16x", "Laptop", 799.0m, 10 },
                    { new Guid("b143a6ef-ec40-4875-8e60-c4f33ca35a53"), "Coolermaster CK550", "Keyboard", 80.0m, 1 }
>>>>>>>> main:BallCom/InventoryService/Migrations/20240621075744_initialCreate.cs
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEvents");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
