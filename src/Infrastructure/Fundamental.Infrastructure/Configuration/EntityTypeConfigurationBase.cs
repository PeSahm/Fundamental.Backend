using Fundamental.Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration;

public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property<ulong>("_id")
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasColumnOrder(0)
            .UseIdentityColumn()
            ;

        builder.HasKey("_id");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnOrder(1);

        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasColumnName("CreatedAt");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone")
            .HasColumnName("ModifiedAt");

        ExtraConfigure(builder);
    }

    protected abstract void ExtraConfigure(EntityTypeBuilder<TEntity> builder);
}