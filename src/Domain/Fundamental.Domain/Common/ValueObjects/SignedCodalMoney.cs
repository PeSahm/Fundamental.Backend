using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Common.ValueObjects;

public class SignedCodalMoney
{
    private decimal _value;

    public SignedCodalMoney(decimal amount, IsoCurrency currency)
    {
        _value = amount * CodalMoneyMultiplier;
        Currency = currency;
    }

    public SignedCodalMoney(decimal amount)
    {
        _value = amount * CodalMoneyMultiplier;
        Currency = AppConfig.BASE_CURRENCY;
    }

    private SignedCodalMoney()
    {
        SetInternally = true;
    }

    public static decimal CodalMoneyMultiplier => 1_000_000;
    private bool SetInternally { get; set; }

    public decimal Value => SetInternally ? _value : _value / CodalMoneyMultiplier;

    public IsoCurrency Currency { get; }

    public static SignedCodalMoney operator +(SignedCodalMoney a, SignedCodalMoney b)
    {
        if (a.Currency != b.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
        }

        return new SignedCodalMoney(a.Value + b.Value, a.Currency);
    }

    public static SignedCodalMoney operator -(SignedCodalMoney a, SignedCodalMoney b)
    {
        if (a.Currency != b.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
        }

        return new SignedCodalMoney(a.Value - b.Value, a.Currency);
    }

    public bool GreaterThan(SignedCodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value > other.Value;
    }

    public bool GreaterThanOrEqual(SignedCodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value == other.Value || GreaterThan(other);
    }

    public bool LessThan(SignedCodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value < other.Value;
    }

    public bool LessThanOrEqual(SignedCodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value == other.Value || LessThan(other);
    }

    public static SignedCodalMoney operator *(decimal a, SignedCodalMoney b)
    {
        return new SignedCodalMoney(b.Value * a, b.Currency);
    }

    public static SignedCodalMoney operator *(SignedCodalMoney a, decimal b)
    {
        return new SignedCodalMoney(a.Value * b, a.Currency);
    }

    public static SignedCodalMoney BaseCurrency(decimal amount)
    {
        return new SignedCodalMoney(amount, AppConfig.BASE_CURRENCY);
    }

    public override string ToString()
    {
        return $"{Value} {Currency.ToString()}";
    }

    /// <summary>
    /// Mix money value and currency.
    /// </summary>
    /// <param name="places">Number of decimal places.</param>
    /// <returns>A string object with money format.</returns>
    public string ToString(short places)
    {
        return $"{Value.ToString($"F{places}")} {Currency.ToString()}";
    }

    public static implicit operator decimal(SignedCodalMoney money) => money.Value;
    public static implicit operator SignedCodalMoney(decimal money) => new(money);

    // public static implicit operator CodalMoney(SignedCodalMoney money) => new(money.Value, money.Currency);
    //
    // public static implicit operator SignedCodalMoney(CodalMoney money) => new(money.Value, money.Currency);
}