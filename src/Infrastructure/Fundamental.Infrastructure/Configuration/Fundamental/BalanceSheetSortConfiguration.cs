using Fundamental.Domain.Codals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class BalanceSheetSortConfiguration : EntityTypeConfigurationBase<BalanceSheetSort>
{
    protected override void ExtraConfigure(EntityTypeBuilder<BalanceSheetSort> builder)
    {
        builder.ToTable("BalanceSheet", "sort");

        builder.Property(x => x.Order)
            .HasColumnName("Order")
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.CodalRow)
            .HasColumnName("CodalRow")
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasColumnName("Category")
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder.HasIndex(x => new { x.Order }).IsUnique();

        builder.HasIndex(x => new { x.Category, x.CodalRow }).IsUnique();
    }
}