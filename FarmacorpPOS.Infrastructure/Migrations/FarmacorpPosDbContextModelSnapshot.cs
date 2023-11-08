﻿// <auto-generated />
using System;
using FarmacorpPOS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmacorpPOS.Infrastructure.Migrations
{
    [DbContext(typeof(FarmacorpPosDbContext))]
    partial class FarmacorpPosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FarmacorpPOS.Domain.ERP.BarCode", b =>
                {
                    b.Property<int>("BarCodeId")
                        .HasColumnType("int");

                    b.Property<Guid>("BarCodeUniqueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("BarCodeId");

                    b.ToTable("BarCodes");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.ERP.ErpProduct", b =>
                {
                    b.Property<int>("ErpProductId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<Guid>("UniqueCode")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErpProductId");

                    b.ToTable("ErpProducts");

                    b.HasData(
                        new
                        {
                            ErpProductId = 1,
                            Cost = 5.9900000000000002,
                            RegistrationDate = new DateTime(2023, 11, 7, 23, 31, 45, 817, DateTimeKind.Local).AddTicks(2319),
                            Stock = 100,
                            UniqueCode = new Guid("02188133-4972-4406-b03c-a5b7fed4b938")
                        });
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Limpieza",
                            IsActive = true
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Lacteos",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.ExpressSale", b =>
                {
                    b.Property<int>("ExpressSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpressSaleId"));

                    b.Property<string>("ClientFullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TotalPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UniqueProductCode")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExpressSaleId");

                    b.HasIndex("ProductId");

                    b.ToTable("ExpressSale");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.JoinEntities.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductCategoryId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("IdCategory");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            ExpirationDate = new DateTime(2023, 11, 7, 23, 31, 45, 817, DateTimeKind.Local).AddTicks(2300),
                            Observations = "Secadores absorbe todo",
                            Price = 10.99,
                            ProductName = "Secadores de Mano",
                            ProductTypeId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            ExpirationDate = new DateTime(2023, 11, 7, 23, 31, 45, 817, DateTimeKind.Local).AddTicks(2309),
                            Observations = "Alimento frutal bebible",
                            Price = 1.5,
                            ProductName = "Pilfrut",
                            ProductTypeId = 2
                        });
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductTypeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductTypeId");

                    b.ToTable("productsType");

                    b.HasData(
                        new
                        {
                            ProductTypeId = 1,
                            Description = "Productos de Limpieza para el hogar"
                        },
                        new
                        {
                            ProductTypeId = 2,
                            Description = "Productos Lacteos"
                        });
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.ERP.BarCode", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.Product", "Product")
                        .WithOne("BarCode")
                        .HasForeignKey("FarmacorpPOS.Domain.ERP.BarCode", "BarCodeId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.ERP.ErpProduct", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.Product", "Product")
                        .WithOne("ErpProduct")
                        .HasForeignKey("FarmacorpPOS.Domain.ERP.ErpProduct", "ErpProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Category", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.ExpressSale", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.Product", "Product")
                        .WithMany("ExpressSales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.JoinEntities.ProductCategory", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmacorpPOS.Domain.Express.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Product", b =>
                {
                    b.HasOne("FarmacorpPOS.Domain.Express.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Category", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.Product", b =>
                {
                    b.Navigation("BarCode");

                    b.Navigation("ErpProduct");

                    b.Navigation("ExpressSales");

                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("FarmacorpPOS.Domain.Express.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
