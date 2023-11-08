using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmacorpPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "productsType",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productsType", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_productsType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "productsType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarCodes",
                columns: table => new
                {
                    BarCodeId = table.Column<int>(type: "int", nullable: false),
                    BarCodeUniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCodes", x => x.BarCodeId);
                    table.ForeignKey(
                        name: "FK_BarCodes_products_BarCodeId",
                        column: x => x.BarCodeId,
                        principalTable: "products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ErpProducts",
                columns: table => new
                {
                    ErpProductId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpProducts", x => x.ErpProductId);
                    table.ForeignKey(
                        name: "FK_ErpProducts_products_ErpProductId",
                        column: x => x.ErpProductId,
                        principalTable: "products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ExpressSale",
                columns: table => new
                {
                    ExpressSaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueProductCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpressSale", x => x.ExpressSaleId);
                    table.ForeignKey(
                        name: "FK_ExpressSale_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "IsActive", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Limpieza", true, null },
                    { 2, "Lacteos", true, null }
                });

            migrationBuilder.InsertData(
                table: "productsType",
                columns: new[] { "ProductTypeId", "Description" },
                values: new object[,]
                {
                    { 1, "Productos de Limpieza para el hogar" },
                    { 2, "Productos Lacteos" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "ExpirationDate", "Observations", "Price", "ProductName", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 7, 20, 47, 52, 150, DateTimeKind.Local).AddTicks(9357), "Secadores absorbe todo", 10.99, "Secadores de Mano", 1 },
                    { 2, new DateTime(2023, 11, 7, 20, 47, 52, 150, DateTimeKind.Local).AddTicks(9369), "Alimento frutal bebible", 1.5, "Pilfrut", 2 }
                });

            migrationBuilder.InsertData(
                table: "ErpProducts",
                columns: new[] { "ErpProductId", "Cost", "RegistrationDate", "Stock" },
                values: new object[] { 1, 5.99m, new DateTime(2023, 11, 7, 20, 47, 52, 150, DateTimeKind.Local).AddTicks(9384), 100 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpressSale_ProductId",
                table: "ExpressSale",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_IdCategory",
                table: "ProductCategory",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductTypeId",
                table: "products",
                column: "ProductTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarCodes");

            migrationBuilder.DropTable(
                name: "ErpProducts");

            migrationBuilder.DropTable(
                name: "ExpressSale");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "productsType");
        }
    }
}
