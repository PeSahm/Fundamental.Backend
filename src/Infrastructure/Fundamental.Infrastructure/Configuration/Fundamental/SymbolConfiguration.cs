using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SymbolConfiguration : EntityTypeConfigurationBase<Symbol>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Symbol> builder)
    {
        builder.ToTable("Symbol", "shd");

        builder.Property(x => x.Isin)
            .HasColumnType("varchar(15)")
            .HasColumnName("Isin")
            .IsRequired();

        builder.HasIndex(x => x.Isin).IsUnique();

        builder.Property(x => x.TseInsCode)
            .HasColumnType("varchar(40)")
            .HasColumnName("TseInsCode")
            .IsRequired();

        builder.Property(x => x.EnName)
            .HasColumnType("varchar(100)")
            .HasColumnName("EnName")
            .IsRequired();

        builder.Property(x => x.SymbolEnName)
            .HasColumnType("varchar(100)")
            .HasColumnName("SymbolEnName")
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnType("nvarchar(100)")
            .HasColumnName("Title")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType("nvarchar(100)")
            .HasColumnName("Name")
            .IsRequired();

        builder.HasIndex(x => x.Name);

        builder.Property(x => x.CompanyEnCode)
            .HasColumnType("nvarchar(100)")
            .HasColumnName("CompanyEnCode")
            .IsRequired();

        builder.Property(x => x.CompanyPersianName)
            .HasColumnType("nvarchar(100)")
            .HasColumnName("CompanyPersianName")
            .IsRequired();

        builder.Property(x => x.CompanyIsin)
            .HasColumnType("nvarchar(100)")
            .HasColumnName("CompanyIsin")
            .IsRequired(false);

        builder.Property(x => x.MarketCap)
            .HasColumnType("bigint")
            .HasColumnName("MarketCap")
            .IsRequired();

        builder.Property(x => x.SectorCode)
            .HasColumnType("varchar(50)")
            .HasColumnName("SectorCode")
            .IsRequired(false);

        builder.Property(x => x.SubSectorCode)
            .HasColumnType("varchar(50)")
            .HasColumnName("SubSectorCode")
            .IsRequired(false);

        builder.Property(x => x.ProductType)
            .HasColumnName("ProductType")
            .HasColumnType("smallint")
            .IsRequired();
    }
}