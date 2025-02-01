using Fundamental.Domain.Symbols.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration.Fundamental;

public class IndexCompanyConfiguration : EntityTypeConfigurationBase<IndexCompany>
{
    protected override void ExtraConfigure(EntityTypeBuilder<IndexCompany> builder)
    {
        builder.ToTable("index_company", "shd");

        builder.HasOne(x => x.Index)
            .WithMany()
            .HasForeignKey("index_id")
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey("company_id")
            .IsRequired();
    }
}