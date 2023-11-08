using FarmacorpPOS.Domain.ERP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmacorpPOS.Infrastructure.Persistence.Configurations
{
    public class BarCodeEntityTypeConfiguration : IEntityTypeConfiguration<BarCode>
    {
        public void Configure(EntityTypeBuilder<BarCode> builder)
        {
            builder
                .HasOne(e => e.Product)
                .WithOne(e => e.BarCode)
                .HasForeignKey<BarCode>(e => e.BarCodeId)
                .IsRequired(false);
        }
    }
}
