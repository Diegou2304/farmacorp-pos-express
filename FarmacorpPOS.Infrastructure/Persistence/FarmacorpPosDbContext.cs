using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;



namespace FarmacorpPOS.Infrastructure.Persistence
{
    public class FarmacorpPosDbContext : DbContext
    {
        public FarmacorpPosDbContext(DbContextOptions<FarmacorpPosDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            new BarCodeEntityTypeConfiguration()
                .Configure(builder.Entity<BarCode>());

            new CategoryEntityTypeConfiguration()
                .Configure(builder.Entity<Category>());

            new ProductEntityTypeConfiguration()
                .Configure(builder.Entity<Product>());

            builder.Entity<ProductType>().HasData(
                new ProductType { ProductTypeId = 1, Description = "Productos de Limpieza para el hogar" },
                new ProductType { ProductTypeId = 2, Description = "Productos Lacteos" }
                );
            builder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Description = "Limpieza", IsActive = true },
               new Category { CategoryId = 2, Description = "Lacteos", IsActive = true }
           );
            builder.Entity<Product>().HasData(
               new Product { ProductId = 1, ProductName = "Secadores de Mano", Price = 9, ExpirationDate = DateTime.Now, Observations = "Secadores absorbe todo", ProductTypeId = 1 }
           );
            builder.Entity<ErpProduct>().HasData(
                new ErpProduct { ErpProductId = 1, Cost = 6, RegistrationDate = DateTime.Now, UniqueCode = Guid.NewGuid(), Stock = 100 }
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
