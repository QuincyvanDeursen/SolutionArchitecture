using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class Decreaseandproductupdateadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5cbac35f-ed55-4816-bea8-ad255963a37e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d6aebfc2-0600-4f10-bc55-f71d98573a74"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("09a5d14f-a802-42ea-a292-52b7fe2f7667"), "Description 1", "Product 1", 10m, 10 },
                    { new Guid("9db90037-da44-4a74-a499-ef4f60860439"), "Description 2", "Product 2", 20m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("09a5d14f-a802-42ea-a292-52b7fe2f7667"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9db90037-da44-4a74-a499-ef4f60860439"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("5cbac35f-ed55-4816-bea8-ad255963a37e"), "Description 1", "Product 1", 10m, 10 },
                    { new Guid("d6aebfc2-0600-4f10-bc55-f71d98573a74"), "Description 2", "Product 2", 20m, 20 }
                });
        }
    }
}
