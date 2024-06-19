using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
