using Fundamental.Domain.Common.Enums;

namespace Fundamental.Domain.Common.Exceptions;

public class MoneyCurrencyIsIncompatibleException : Exception
{
    public MoneyCurrencyIsIncompatibleException(IsoCurrency baseCurrency, IsoCurrency otherCurrency)
        : base($"Money currency {otherCurrency} is incompatible with base currency {baseCurrency}.")
    {
    }
}