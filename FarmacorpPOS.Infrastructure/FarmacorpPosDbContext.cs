

using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Domain.Express.JoinEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace FarmacorpPOS.Infrastructure
{
    public class FarmacorpPosDbContext : DbContext
    {
        public FarmacorpPosDbContext(DbContextOptions<FarmacorpPosDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(x => x.ProductType)
                .WithMany(x => x.Products);

            builder.Entity<Product>()
                .HasMany(x => x.ExpressSales)
                .WithOne(x => x.Product);

            builder.Entity<Product>()
                    .HasMany(e => e.Categories)
                    .WithMany(e => e.Products)
                    .UsingEntity<ProductCategory>();

            builder.Entity<Product>()
                   .HasOne(e => e.BarCode)
                   .WithOne(e => e.Product)
                   .HasForeignKey<BarCode>(e => e.BarCodeId)
                   .IsRequired(false);

            builder.Entity<Product>()
              .HasOne(e => e.ErpProduct)
              .WithOne(e => e.Product)
              .HasForeignKey<ErpProduct>(e => e.ErpProductId)
              .IsRequired(false);

            builder.Entity<Category>()
                .Property(c => c.ParentCategoryId)
                .IsRequired(false);
                
            builder.Entity<Category>()   
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProductType>().HasData(
                new ProductType { ProductTypeId = 1, Description = "Productos de Limpieza para el hogar"},
                new ProductType { ProductTypeId = 2, Description = "Productos Lacteos"}
                );
            builder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Description = "Limpieza", IsActive = true },
               new Category { CategoryId = 2, Description = "Lacteos", IsActive = true }
           );
            builder.Entity<Product>().HasData(
               new Product { ProductId = 1, ProductName = "Secadores de Mano", Price = 10.99, ExpirationDate = DateTime.Now, Observations = "Secadores absorbe todo", ProductTypeId = 1 },
               new Product { ProductId = 2, ProductName = "Pilfrut", Price = 1.50, ExpirationDate = DateTime.Now, Observations = "Alimento frutal bebible", ProductTypeId = 2 }
           );
            builder.Entity<ErpProduct>().HasData(
                new ErpProduct { ErpProductId = 1, Cost = 5.99m, RegistrationDate = DateTime.Now, Stock = 100 }
           );

           builder.Entity<BarCode>().HasData(
                new BarCode { BarCodeId = 1, BarCodeUniqueId = Guid.NewGuid(), IsActive = true }
            );

        }
    

        public DbSet<BarCode> BarCodes { get; set; }
        public DbSet<ErpProduct> ErpProducts { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExpressSale> ExpressSale { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductType> productsType { get; set; }

    }
}
