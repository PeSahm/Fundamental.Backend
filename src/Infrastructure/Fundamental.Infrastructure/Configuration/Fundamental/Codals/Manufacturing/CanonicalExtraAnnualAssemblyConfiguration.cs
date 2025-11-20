using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

/// <summary>
/// EF Core configuration for CanonicalExtraAnnualAssembly entity.
/// </summary>
public class CanonicalExtraAnnualAssemblyConfiguration : EntityTypeConfigurationBase<CanonicalExtraAnnualAssembly>
{
    /// <summary>
    /// Configures EF Core mappings for CanonicalExtraAnnualAssembly, including table name and schema, relationships, scalar properties, value objects, and JSON-backed owned entities and collections.
    /// </summary>
    /// <remarks>
    /// Maps the entity to the "manufacturing" schema using snake_case naming, configures a required foreign key to Symbol, applies constraints for version, dates and URLs, maps value objects (fiscal year, year/report months), applies currency configuration, and persists several owned entities and collections as JSON/JSONB columns.
    /// </remarks>
    protected override void ExtraConfigure(EntityTypeBuilder<CanonicalExtraAnnualAssembly> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(CanonicalExtraAnnualAssembly)), "manufacturing");

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

        // Additional meeting information
        builder.Property(x => x.BoardMemberPeriod);

        builder.Property(x => x.PublishSecurityDescription);

        builder.Property(x => x.OtherDescription);

        builder.Property(x => x.NewHour);

        builder.Property(x => x.NewDay);

        builder.Property(x => x.NewDate);

        builder.Property(x => x.NewLocation);

        builder.Property(x => x.BreakDescription);

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
            x => x.Inspectors,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("inspectors");
            });

        builder.OwnsMany(
            x => x.NewBoardMembers,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("new_board_members");
            });

        builder.OwnsMany(
            x => x.BoardMemberWageAndGifts,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("board_member_wage_and_gifts");
            });

        builder.OwnsMany(
            x => x.NewsPapers,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("news_papers");
            });

        builder.OwnsMany(
            x => x.AssemblyInterims,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("assembly_interims");
            });

        builder.OwnsMany(
            x => x.ProportionedRetainedEarnings,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("proportioned_retained_earnings");
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

        builder.OwnsOne(
            x => x.IndependentAuditorRepresentative,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("independent_auditor_representative");
            });

        builder.OwnsOne(
            x => x.TopFinancialPosition,
            navigationBuilder =>
            {
                navigationBuilder.ToJson("top_financial_position");
            });
    }
}