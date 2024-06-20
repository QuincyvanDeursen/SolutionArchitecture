using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("78151e52-a564-460e-8d4d-7d58bcd49890"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("82b5a43e-0ab3-4ac7-9dc2-e9ed2b2d22d9"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("ea043a64-8f06-41fb-9b54-b143d911bcfb"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CompanyName", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("1d11cc97-809b-45b1-b096-516140f71f93"), "Avansstraat 123, 1234AB Breda", "Willy's", "Tristan", "Goossens", "0612345678" },
                    { new Guid("532cd2ef-1ed8-4624-b780-101e34d28a8e"), "Avansstraat 123, 1234AB Breda", "Dropshipper", "Thimo", "Luijsterburg", "0612345678" },
                    { new Guid("9b9c24d5-1f47-40ef-800b-09cfe8e5c89b"), "Avansstraat 123, 1234AB Breda", "ICTutor", "Sybrand", "Bos", "0612345678" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("1d11cc97-809b-45b1-b096-516140f71f93"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("532cd2ef-1ed8-4624-b780-101e34d28a8e"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("9b9c24d5-1f47-40ef-800b-09cfe8e5c89b"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CompanyName", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("78151e52-a564-460e-8d4d-7d58bcd49890"), "Avansstraat 123, 1234AB Breda", "Willy's", "Tristan", "Goossens", "" },
                    { new Guid("82b5a43e-0ab3-4ac7-9dc2-e9ed2b2d22d9"), "Avansstraat 123, 1234AB Breda", "ICTutor", "Sybrand", "Bos", "" },
                    { new Guid("ea043a64-8f06-41fb-9b54-b143d911bcfb"), "Avansstraat 123, 1234AB Breda", "Dropshipper", "Thimo", "Luijsterburg", "" }
                });
        }
    }
}
