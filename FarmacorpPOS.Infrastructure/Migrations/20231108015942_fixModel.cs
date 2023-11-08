using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmacorpPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductCategory");

            migrationBuilder.UpdateData(
                table: "ErpProducts",
                keyColumn: "ErpProductId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 11, 7, 21, 59, 42, 574, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2023, 11, 7, 21, 59, 42, 574, DateTimeKind.Local).AddTicks(4300));

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2023, 11, 7, 21, 59, 42, 574, DateTimeKind.Local).AddTicks(4311));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ErpProducts",
                keyColumn: "ErpProductId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 11, 7, 21, 1, 59, 403, DateTimeKind.Local).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2023, 11, 7, 21, 1, 59, 403, DateTimeKind.Local).AddTicks(3244));

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2023, 11, 7, 21, 1, 59, 403, DateTimeKind.Local).AddTicks(3254));
        }
    }
}
