using FarmacorpPOS.Domain.Express;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FarmacorpPOS.Infrastructure.Persistence.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(c => c.ParentCategoryId)
                .IsRequired(false);

            builder
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
