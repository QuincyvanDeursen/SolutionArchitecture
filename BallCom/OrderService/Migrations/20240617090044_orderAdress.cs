using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class orderAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postalcode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Adress", "City", "OrderDate", "Postalcode" },
                values: new object[] { "Pelmolenstraat 11A", "Breda", new DateTime(2024, 6, 17, 11, 0, 43, 991, DateTimeKind.Local).AddTicks(167), "4811LR" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Adress", "City", "OrderDate", "Postalcode" },
                values: new object[] { "Pelmolenstraat 101A", "Breda", new DateTime(2024, 6, 17, 11, 0, 43, 991, DateTimeKind.Local).AddTicks(212), "4812LR" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Adress", "City", "OrderDate", "Postalcode" },
                values: new object[] { "Pelmolenstraat 111A", "Breda", new DateTime(2024, 6, 17, 11, 0, 43, 991, DateTimeKind.Local).AddTicks(215), "4813LR" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Postalcode",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 6, 14, 17, 6, 1, 722, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 6, 14, 17, 6, 1, 722, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2024, 6, 14, 17, 6, 1, 722, DateTimeKind.Local).AddTicks(5487));
        }
    }
}
