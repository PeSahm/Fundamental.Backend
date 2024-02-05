using Fundamental.Domain.Prices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class ClosePriceConfiguration : EntityTypeConfigurationBase<ClosePrice>
{
    protected override void ExtraConfigure(EntityTypeBuilder<ClosePrice> builder)
    {
        builder.ToTable("close-price", "shd");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .IsRequired()
            .HasForeignKey("symbol_id");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnName("date")
            .HasColumnType(NpgsqlTypes.NpgsqlDbType.Date.ToString());

        builder.Property(x => x.Close)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Open)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.High)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Low)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Last)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.CloseAdjusted)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.OpenAdjusted)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.HighAdjusted)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.LowAdjusted)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.LastAdjusted)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Volume)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("bigint");
    }
}