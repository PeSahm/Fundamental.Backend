using Fundamental.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundamental.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static PropertyBuilder<TProperty> Currency<TProperty>(this PropertyBuilder<TProperty> builder, string columnName = "Currency")
    {
        return builder.HasColumnName(columnName)
            .HasColumnType("char(3)")
            .IsRequired()
            .IsFixedLength()
            .HasMaxLength(3)
            .HasConversion<CurrencyConverter>();
    }

    public static PropertyBuilder<TProperty> UseCurrencyColumn<TProperty>(
        this PropertyBuilder<TProperty> builder,
        string columnName = "Currency"
    )
    {
        return builder.HasColumnName(columnName)
            .HasConversion<CurrencyConverter>();
    }

    public static ComplexTypePropertyBuilder UseCurrencyColumn(
        this ComplexTypePropertyBuilder builder,
        string columnName = "Currency"
    )
    {
        return builder.HasColumnName(columnName)
            .HasConversion<CurrencyConverter>();
    }
}