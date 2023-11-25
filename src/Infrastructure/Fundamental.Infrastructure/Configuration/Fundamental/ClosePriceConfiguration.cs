using Fundamental.Domain.Prices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class ClosePriceConfiguration : EntityTypeConfigurationBase<ClosePrice>
{
    protected override void ExtraConfigure(EntityTypeBuilder<ClosePrice> builder)
    {
        builder.ToTable("ClosePrice", "shd");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .IsRequired()
            .HasForeignKey("SymbolId");

        builder.HasIndex("Date", "SymbolId").IsUnique();

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnName("Date")
            .HasColumnType("date")
            .HasColumnOrder(3);

        builder.Property(x => x.Close)
            .IsRequired()
            .HasColumnName("Close")
            .HasColumnType("bigint")
            .HasColumnOrder(4);

        builder.Property(x => x.Open)
            .IsRequired()
            .HasColumnName("Open")
            .HasColumnType("bigint")
            .HasColumnOrder(5);

        builder.Property(x => x.High)
            .IsRequired()
            .HasColumnName("High")
            .HasColumnType("bigint")
            .HasColumnOrder(6);

        builder.Property(x => x.Low)
            .IsRequired()
            .HasColumnName("Low")
            .HasColumnType("bigint")
            .HasColumnOrder(7);

        builder.Property(x => x.Last)
            .IsRequired()
            .HasColumnName("Last")
            .HasColumnType("bigint")
            .HasColumnOrder(8);

        builder.Property(x => x.CloseAdjusted)
            .IsRequired()
            .HasColumnName("CloseAdjusted")
            .HasColumnType("bigint")
            .HasColumnOrder(9);

        builder.Property(x => x.OpenAdjusted)
            .IsRequired()
            .HasColumnName("OpenAdjusted")
            .HasColumnType("bigint")
            .HasColumnOrder(10);

        builder.Property(x => x.HighAdjusted)
            .IsRequired()
            .HasColumnName("HighAdjusted")
            .HasColumnType("bigint")
            .HasColumnOrder(11);

        builder.Property(x => x.LowAdjusted)
            .IsRequired()
            .HasColumnName("LowAdjusted")
            .HasColumnType("bigint")
            .HasColumnOrder(12);

        builder.Property(x => x.LastAdjusted)
            .IsRequired()
            .HasColumnName("LastAdjusted")
            .HasColumnType("bigint")
            .HasColumnOrder(13);

        builder.Property(x => x.Volume)
            .IsRequired()
            .HasColumnName("Volume")
            .HasColumnType("bigint")
            .HasColumnOrder(14);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("bigint")
            .HasColumnOrder(15);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnName("Value")
            .HasColumnType("bigint")
            .HasColumnOrder(16);
    }
}