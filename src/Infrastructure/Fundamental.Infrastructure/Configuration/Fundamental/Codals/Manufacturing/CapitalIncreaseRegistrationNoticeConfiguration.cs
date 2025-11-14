using Fundamental.Domain.Codals.Manufacturing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;
using NpgsqlTypes;

namespace Fundamental.Infrastructure.Configuration.Fundamental.Codals.Manufacturing;

public class CapitalIncreaseRegistrationNoticeConfiguration : EntityTypeConfigurationBase<CapitalIncreaseRegistrationNotice>
{
    protected override void ExtraConfigure(EntityTypeBuilder<CapitalIncreaseRegistrationNotice> builder)
    {
        builder.ToTable(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(nameof(CapitalIncreaseRegistrationNotice)), "manufacturing");

        builder.HasOne(x => x.Symbol)
            .WithMany()
            .HasForeignKey("symbol_id")
            .IsRequired();

        builder.Property(x => x.TraceNo)
            .HasColumnType(nameof(NpgsqlDbType.Bigint))
            .IsRequired();

        builder.Property(x => x.Uri)
            .HasMaxLength(512)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Currency)
            .Currency();

        builder.Property(x => x.StartDate).HasColumnType(nameof(NpgsqlDbType.Date));
        builder.Property(x => x.LastExtraAssemblyDate).HasColumnType(nameof(NpgsqlDbType.Date));

        builder.ComplexProperty(x => x.NewCapital, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.PreviousCapital, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.Reserves, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.RetainedEarning, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.RevaluationSurplus, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.CashIncoming, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.SarfSaham, propertyBuilder => propertyBuilder.UseCodalMoney());
        builder.ComplexProperty(x => x.CashForceclosurePriority, propertyBuilder => propertyBuilder.UseCodalMoney());

        builder.Property(x => x.PrimaryMarketTracingNo)
            .HasColumnType(nameof(NpgsqlDbType.Bigint))
            .IsRequired();
    }
}