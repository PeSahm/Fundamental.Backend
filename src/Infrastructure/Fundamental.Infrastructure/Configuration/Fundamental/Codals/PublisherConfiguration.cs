using Fundamental.Domain.Codals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals;

public class PublisherConfiguration : EntityTypeConfigurationBase<Publisher>
{
    protected override void ExtraConfigure(EntityTypeBuilder<Publisher> builder)
    {
        builder.ToTable("publisher", "fs");

        builder.Property(x => x.CodalId).IsRequired().HasMaxLength(128);

        builder.HasOne(x => x.Symbol).WithOne(x => x.Publisher).HasForeignKey<Publisher>("symbol_id").IsRequired();

        builder.HasOne(x => x.ParentSymbol).WithMany().HasForeignKey("parent_symbol_id").IsRequired(false);

        builder.Property(x => x.Isic).HasMaxLength(128).IsRequired(false);

        builder.Property(x => x.ReportingType).IsRequired();

        builder.Property(x => x.CompanyType).IsRequired();

        builder.Property(x => x.ExecutiveManager).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.Address).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.TelNo).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.FaxNo).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.ActivitySubject).HasMaxLength(2048).IsRequired(false);

        builder.Property(x => x.OfficeAddress).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.ShareOfficeAddress).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.Website).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.Email).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.Inspector).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.FinancialManager).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.FactoryTel).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.FactoryFax).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.OfficeTel).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.OfficeFax).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.ShareOfficeTel).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.ShareOfficeFax).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.NationalCode).HasMaxLength(15).IsRequired(false);

        builder.Property(x => x.FinancialYear).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.AuditorName).HasMaxLength(512).IsRequired(false);

        builder.Property(x => x.IsEnableSubCompany).IsRequired();

        builder.Property(x => x.IsEnabled).IsRequired();

        builder.Property(x => x.FundType).IsRequired();

        builder.Property(x => x.SubCompanyType).IsRequired();

        builder.Property(x => x.IsSupplied).IsRequired();

        builder.Property(x => x.MarketType).IsRequired();

        builder.ComplexProperty(
            x => x.UnauthorizedCapital,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.ComplexProperty(
            x => x.ListedCapital,
            navigationBuilder => navigationBuilder.UseCodalMoney());

        builder.Property(x => x.State).IsRequired();

        builder.Property(x => x.Currency)
            .Currency();
    }
}