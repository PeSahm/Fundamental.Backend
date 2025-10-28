using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class IncomeStatementDetailConfiguration : EntityTypeConfigurationBase<IncomeStatementDetail>
{
    protected override void ExtraConfigure(EntityTypeBuilder<IncomeStatementDetail> builder)
    {
        builder.ToTable("income_statement_details", "manufacturing");
        builder.HasOne(x => x.IncomeStatement)
            .WithMany(x => x.Details)
            .HasForeignKey("income_statement_id")
            .IsRequired();

        builder.Property(x => x.Row).IsRequired();

        builder.Property(x => x.CodalRow).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

        builder.ComplexProperty(
            x => x.Value,
            amount => amount.UseSignedCodalMoney());
    }
}