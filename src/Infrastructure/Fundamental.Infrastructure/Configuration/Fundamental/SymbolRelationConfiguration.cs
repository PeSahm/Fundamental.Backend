using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SymbolRelationConfiguration : EntityTypeConfigurationBase<SymbolRelation>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolRelation> builder)
    {
        builder.ToTable("symbol-relation", "shd");

        builder.Property(x => x.Ratio)
            .HasPrecision(18, 3)
            .IsRequired();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.InvestmentSymbols)
            .HasForeignKey("parent-id")
            .IsRequired();

        builder.HasOne(x => x.Child)
            .WithMany(x => x.InvestorSymbols)
            .HasForeignKey("child-id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction)
            ;
    }
}