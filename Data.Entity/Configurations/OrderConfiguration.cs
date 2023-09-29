using Data.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entity.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CreateAt).HasDefaultValue(DateTime.Now);

            builder.HasOne(x=>x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.UserOrderInformation).WithMany(x => x.Orders).HasForeignKey(x => x.UserOrderInfoId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
