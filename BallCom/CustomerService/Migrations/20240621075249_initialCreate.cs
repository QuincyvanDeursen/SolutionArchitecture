using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                    { new Guid("2cf9b7df-e7fd-461d-8697-500a13826406"), "Avansstraat 123, 1234AB Breda", "Willy's", "Tristan", "Goossens", "0612345678" },
                    { new Guid("d1b96ae1-ec78-43b6-83b5-bce5359b2dc6"), "Avansstraat 123, 1234AB Breda", "Dropshipper", "Thimo", "Luijsterburg", "0612345678" },
                    { new Guid("d43c3429-dd30-4384-baba-6b2eb425bfca"), "Avansstraat 123, 1234AB Breda", "ICTutor", "Sybrand", "Bos", "0612345678" }
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
