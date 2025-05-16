using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.AlcareConfigurations
{
    public class FullEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IFullEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.IsAlive);
            builder.Property(x => x.CreatedDate).IsRequired(false);
            builder.Property(x => x.CreatedUser).IsRequired(false);
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.UpdatedUser).IsRequired(false);
            builder.HasQueryFilter(x => x.IsAlive);
        }
    }
}
