
using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Domain.Express.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmacorpPOS.Infrastructure.Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .HasOne(x => x.ProductType)
               .WithMany(x => x.Products);

            builder
                .HasMany(x => x.ExpressSales)
                .WithOne(x => x.Product);

            builder
                    .HasMany(e => e.Categories)
                    .WithMany(e => e.Products)
                    .UsingEntity<ProductCategory>();

            builder
            .HasOne(e => e.ErpProduct)
            .WithOne(e => e.Product)
            .HasForeignKey<ErpProduct>(e => e.ErpProductId)
            .IsRequired(false);
        }
    }
}
