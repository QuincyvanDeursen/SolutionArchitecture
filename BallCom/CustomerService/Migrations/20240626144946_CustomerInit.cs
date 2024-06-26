using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class CustomerInit : Migration
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
                    { new Guid("5e24d267-480e-4954-8654-0041f5f38f62"), "Avansstraat 123, 1234AB Breda", "Dropshipper", "Thimo", "Luijsterburg", "0612345678" },
                    { new Guid("b30226fe-cb36-451d-b28e-e1158cbea431"), "Avansstraat 123, 1234AB Breda", "Willy's", "Tristan", "Goossens", "0612345678" },
                    { new Guid("e7f53e2b-0e68-4dea-8b81-f636ad4c1e98"), "Avansstraat 123, 1234AB Breda", "ICTutor", "Sybrand", "Bos", "0612345678" }
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
