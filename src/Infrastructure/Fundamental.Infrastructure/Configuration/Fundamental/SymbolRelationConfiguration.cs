using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class SymbolRelationConfiguration : EntityTypeConfigurationBase<SymbolRelation>
{
    protected override void ExtraConfigure(EntityTypeBuilder<SymbolRelation> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(SymbolRelation)), "shd");

        builder.Property(x => x.Ratio)
            .HasPrecision(18, 3)
            .IsRequired();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.InvestmentSymbols)
            .HasForeignKey("parent_id")
            .IsRequired();

        builder.HasOne(x => x.Child)
            .WithMany(x => x.InvestorSymbols)
            .HasForeignKey("child_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction)
            ;
    }
}