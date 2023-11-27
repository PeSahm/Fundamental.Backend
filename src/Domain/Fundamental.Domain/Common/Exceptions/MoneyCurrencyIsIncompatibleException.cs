using Fundamental.Domain.Common.Enums;

namespace Fundamental.Domain.Common.Exceptions;

[Serializable]
public class MoneyCurrencyIsIncompatibleException : Exception
{
    public MoneyCurrencyIsIncompatibleException(IsoCurrency baseCurrency, IsoCurrency otherCurrency)
        : base($"Money currency {otherCurrency} is incompatible with base currency {baseCurrency}.")
    {
    }

    protected MoneyCurrencyIsIncompatibleException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}