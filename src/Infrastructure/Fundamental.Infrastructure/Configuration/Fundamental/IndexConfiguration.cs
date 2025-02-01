using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class IndexConfiguration : EntityTypeConfigurationBase<Index>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Index> builder)
    {
        builder.ToTable("indices", "shd");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .IsRequired()
            .HasForeignKey("symbol_id");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnName("date");

        builder.Property(x => x.Open)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("open");

        builder.Property(x => x.High)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("high");

        builder.Property(x => x.Low)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("low");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("value");
    }
}