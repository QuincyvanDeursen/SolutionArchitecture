using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("0eb2bf9f-20a7-4818-8a40-46477e32c7d9"), "Description 2", "Product 2", 20m, 20 },
                    { new Guid("69f986de-6f38-4d97-93ad-363a7c3888dd"), "Description 1", "Product 1", 10m, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0eb2bf9f-20a7-4818-8a40-46477e32c7d9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("69f986de-6f38-4d97-93ad-363a7c3888dd"));
        }
    }
}
