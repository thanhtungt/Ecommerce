using Data.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entity.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(x => new { x.CategoryId, x.ProductId });

            builder.HasOne(x => x.Product).WithMany(i => i.ProductInCategories).HasForeignKey(i => i.ProductId);
            builder.HasOne(x => x.Category).WithMany(i => i.ProductInCategories).HasForeignKey(i => i.CategoryId);
        }
    }
}
