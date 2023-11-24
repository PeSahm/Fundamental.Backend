using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SymbolRelationConfiguration : EntityTypeConfigurationBase<SymbolRelation>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolRelation> builder)
    {
        builder.Property(x => x.Ratio)
            .HasColumnType("float")
            .HasColumnName("Ratio")
            .IsRequired();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.InvestmentSymbols)
            .HasForeignKey("ParentId")
            .IsRequired();

        builder.HasOne(x => x.Child)
            .WithMany(x => x.InvestorSymbols)
            .HasForeignKey("ChildId")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction)
            ;
    }
}