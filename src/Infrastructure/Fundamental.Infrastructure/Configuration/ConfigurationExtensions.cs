using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
}