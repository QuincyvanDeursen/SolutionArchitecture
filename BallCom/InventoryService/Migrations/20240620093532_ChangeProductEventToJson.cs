using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductEventToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20958433-57e6-405b-8373-e00f45198850"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c60027eb-b622-4aa0-9096-6b285872aa30"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e18f3437-9537-4cc4-85a2-cf94e5d6f626"));

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "InventoryEvents",
                newName: "ProductJson");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("3cc09248-d064-4bf8-bcb6-142283f0d185"), "Logitech MX Master", "Mouse", 40.0m, 0 },
                    { new Guid("f7db078e-66b8-4c86-9af3-a3c4e03f12e1"), "Coolermaster CK550", "Keyboard", 80.0m, 1 },
                    { new Guid("ff7d052d-92e1-4a75-92e7-60691de7757d"), "HP Envy 16x", "Laptop", 799.0m, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3cc09248-d064-4bf8-bcb6-142283f0d185"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f7db078e-66b8-4c86-9af3-a3c4e03f12e1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ff7d052d-92e1-4a75-92e7-60691de7757d"));

            migrationBuilder.RenameColumn(
                name: "ProductJson",
                table: "InventoryEvents",
                newName: "Product");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("20958433-57e6-405b-8373-e00f45198850"), "HP Envy 16x", "Laptop", 799.0m, 10 },
                    { new Guid("c60027eb-b622-4aa0-9096-6b285872aa30"), "Coolermaster CK550", "Keyboard", 80.0m, 1 },
                    { new Guid("e18f3437-9537-4cc4-85a2-cf94e5d6f626"), "Logitech MX Master", "Mouse", 40.0m, 0 }
                });
        }
    }
}
