namespace Fundamental.Domain.Common.ValueObjects;

public record CodalMoney(decimal Value)
{
    private const decimal CODAL_MONEY_MULTIPLIER = 1_000_000;

    public static implicit operator Money(CodalMoney money) => new(money.Value * CODAL_MONEY_MULTIPLIER, AppConfig.BASE_CURRENCY);

    public static implicit operator SignedMoney(CodalMoney money) => new(money.Value * CODAL_MONEY_MULTIPLIER, AppConfig.BASE_CURRENCY);
}