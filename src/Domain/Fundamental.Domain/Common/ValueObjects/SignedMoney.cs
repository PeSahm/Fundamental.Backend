using Fundamental.Domain.Common.Enums;

namespace Fundamental.Domain.Common.ValueObjects;

public record SignedMoney(decimal Value, IsoCurrency Currency)
{
    public static implicit operator Money(SignedMoney money) => new(money.Value, money.Currency);
    public static implicit operator SignedMoney(Money money) => new(money.Value, money.Currency);
}