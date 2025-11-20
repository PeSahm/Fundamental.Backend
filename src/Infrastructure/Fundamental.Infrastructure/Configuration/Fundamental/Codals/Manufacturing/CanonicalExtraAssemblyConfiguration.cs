using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

/// <summary>
/// EF Core configuration for CanonicalExtraAssembly entity.
/// </summary>
public class CanonicalExtraAssemblyConfiguration : EntityTypeConfigurationBase<CanonicalExtraAssembly>
{
    protected override void ExtraConfigure(EntityTypeBuilder<CanonicalExtraAssembly> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(CanonicalExtraAssembly)), "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.Version)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.PublishDate)
            .IsRequired();

        builder.Property(x => x.AssemblyDate)
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .IsRequired();

        builder.Property(x => x.HtmlUrl)
            .IsRequired();

        // Value objects using ComplexProperty
        builder.ComplexProperty(
            x => x.FiscalYear,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Year)
                    .HasColumnName("fiscal_year")
                    .HasColumnType("SMALLINT")
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.YearEndMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("year_end_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.OwnsOne(
            x => x.ReportMonth,
            navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Month)
                    .HasColumnName("report_month")
                    .HasColumnType("smallint")
                    .IsRequired();
            });

        builder.Property(x => x.Currency)
            .Currency();

        // Owned entities (preserve JSON structure)
        builder.OwnsOne(
            x => x.ParentAssemblyInfo,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("parent_assembly_info");
                navigationBuilder.OwnsMany(x => x.SessionOrders);
            });

        builder.OwnsOne(
            x => x.AssemblyChiefMembersInfo,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("assembly_chief_members_info");
            });

        builder.OwnsOne(
            x => x.NextSessionInfo,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("next_session_info");
            });

        builder.OwnsOne(
            x => x.ExtraAssemblyScheduling,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("extra_assembly_scheduling");
            });

        builder.OwnsOne(
            x => x.ExtraAssemblyDecreaseCapital,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("extra_assembly_decrease_capital");
            });

        builder.OwnsOne(
            x => x.ExtraAssemblyShareValueChangeCapital,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("extra_assembly_share_value_change_capital");
            });

        // Root-level properties
        builder.Property(x => x.CapitalChangeState)
            .IsRequired();

        builder.Property(x => x.LastShareValue);

        builder.Property(x => x.LastCapital);

        builder.Property(x => x.LastShareCount);

        builder.Property(x => x.OldAddress);

        builder.Property(x => x.NewAddress);

        builder.Property(x => x.OldName);

        builder.Property(x => x.NewName);

        builder.Property(x => x.OldActivitySubject);

        builder.Property(x => x.NewActivitySubject);

        builder.Property(x => x.OldFinancialYearMonthLength);

        builder.Property(x => x.OldFinancialYearEndDate);

        builder.Property(x => x.OldFinancialYearDayLength);

        builder.Property(x => x.NewFinancialYearEndDate);

        builder.Property(x => x.NewFinancialYearMonthLength);

        builder.Property(x => x.NewFinancialYearDayLength);

        builder.Property(x => x.IsLocationChange);

        builder.Property(x => x.IsNameChange);

        builder.Property(x => x.IsActivitySubjectChange);

        builder.Property(x => x.IsFinancialYearChange);

        builder.Property(x => x.IsDecidedClause141);

        builder.Property(x => x.DecidedClause141Des);

        builder.Property(x => x.IsAccordWithSEOStatuteApproved);

        builder.Property(x => x.OtherDes);

        builder.Property(x => x.PrimaryMarketTracingNo);

        builder.Property(x => x.CorrectionStatuteApproved);

        // Owned collections - stored as JSONB using OwnsMany
        builder.OwnsMany(
            x => x.ShareHolders,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("share_holders");
            });

        builder.OwnsMany(
            x => x.AssemblyBoardMembers,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("assembly_board_members");
            });

        builder.OwnsMany(
            x => x.ExtraAssemblyIncreaseCapitals,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("extra_assembly_increase_capitals");
            });

        // Owned entities (nullable) - stored as JSONB using OwnsOne
        builder.OwnsOne(
            x => x.Ceo,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("ceo");
            });

        builder.OwnsOne(
            x => x.AuditCommitteeChairman,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("audit_committee_chairman");
            });
    }
}
