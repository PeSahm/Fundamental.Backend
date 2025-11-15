using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fundamental.Infrastructure.Persistence.Converters;

public class CurrencyConverter : ValueConverter<IsoCurrency, string>
{
    public CurrencyConverter()
        : base(c => c.ToString(), c => Enum.Parse<IsoCurrency>(c, true))
    {
    }
}