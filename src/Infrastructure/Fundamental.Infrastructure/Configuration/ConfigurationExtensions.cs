using Fundamental.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.NameTranslation;

namespace Fundamental.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static PropertyBuilder<TProperty> Currency<TProperty>(this PropertyBuilder<TProperty> builder, string columnName = "currency")
    {
        return builder.HasColumnName(columnName)
            .IsRequired();
    }

    public static PropertyBuilder<TProperty> UseCurrencyColumn<TProperty>(
        this PropertyBuilder<TProperty> builder,
        string columnName = "currency"
    )
    {
        return builder.HasColumnName(columnName);
    }

    public static ComplexTypePropertyBuilder UseCurrencyColumn(
        this ComplexTypePropertyBuilder builder,
        string columnName = "currency"
    )
    {
        return builder.HasColumnName(columnName);
    }

    public static void UseSignedCodalMoney(this ComplexPropertyBuilder<SignedCodalMoney> builder)
    {
        builder.Property(money => money.Value)
            .HasField("_value")
            .HasColumnName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(builder.Metadata.Name))
            .HasConversion(x => x, x => x )
            .HasColumnType("decimal")
            .HasPrecision(36, 10)
            .IsRequired();

        builder.Property(money => money.Currency)
            .UseCurrencyColumn();
    }

    public static void UseCodalMoney(this ComplexPropertyBuilder<CodalMoney> builder)
    {
        builder.Property(money => money.Value)
            .HasColumnName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(builder.Metadata.Name))
            .HasField("_value")
            .HasConversion(x => x, x => x )
            .HasColumnType("decimal")
            .HasPrecision(36, 10)
            .IsRequired();

        builder.Property(money => money.Currency)
            .UseCurrencyColumn();
    }
}