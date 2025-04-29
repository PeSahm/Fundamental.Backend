using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SymbolConfiguration : EntityTypeConfigurationBase<Symbol>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Symbol> builder)
    {
        builder.ToTable("symbol", "shd");

        builder.Property(x => x.Isin)
            .HasMaxLength(15)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.Isin).IsUnique();

        builder.Property(x => x.TseInsCode)
            .HasMaxLength(40)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.EnName)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.SymbolEnName)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name);

        builder.Property(x => x.CompanyEnCode)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.CompanyPersianName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CompanyIsin)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.MarketCap)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.SectorCode)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.SubSectorCode)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ProductType)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.ProductType2)
            .HasDefaultValue(ProductType.All)
            .HasSentinel(ProductType.All)
            .IsRequired();

        builder.Property(x => x.ExchangeType)
            .HasDefaultValue(ExchangeType.None)
            .HasSentinel(ExchangeType.None)
            .IsRequired();

        builder.Property(x => x.EtfType)
            .IsRequired(false);

        builder.HasIndex(x => new { x.SectorCode, x.SubSectorCode })
            .HasDatabaseName("ix_symbol_sector_codes");
    }
}