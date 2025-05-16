using Entities.Buffalo;
using Entities.AlcareConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ReactConfigurations
{
    public class UserConfiguration : FullEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(16);
            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.LastName).IsRequired(false);
            builder.Property(x => x.FullName).IsRequired(false);
            builder.Property(x => x.UserImageURL).IsRequired(false);
        }
    }
}
