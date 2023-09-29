using Data.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entity.Configurations
{
    public class UserOrderInformationConfiguration : IEntityTypeConfiguration<UserOrderInformation>
    {
        public void Configure(EntityTypeBuilder<UserOrderInformation> builder)
        {
            builder.ToTable("UserOrderInformations");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AppUser).WithMany(x => x.UserOrderInformations).HasForeignKey(z => z.UserId);
        }
    }
}
