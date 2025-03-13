using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Common.ValueObjects;

public sealed class CodalMoney : IEquatable<CodalMoney>
{
    private readonly decimal _value;

    public static CodalMoney Empty => new();

    public CodalMoney(decimal amount, IsoCurrency currency)
    {
        if (amount < 0)
        {
            throw new InvalidMoneyAmountException(amount);
        }

        _value = amount * CodalMoneyMultiplier;
        Currency = currency;
        SetInternally = false;
    }

    public CodalMoney(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidMoneyAmountException(amount);
        }

        _value = amount * CodalMoneyMultiplier;
        Currency = AppConfig.BASE_CURRENCY;
        SetInternally = false;
    }

    private CodalMoney()
    {
        SetInternally = true;
    }

    public static decimal CodalMoneyMultiplier => 1_000_000;

    public decimal Value => _value / CodalMoneyMultiplier;

    public decimal RealValue => _value;

    public IsoCurrency Currency
    {
        get;
    }

    private bool SetInternally { get; set; }

    public static CodalMoney operator +(CodalMoney a, CodalMoney b)
    {
        if (a.Currency != b.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
        }

        return new CodalMoney(a.Value + b.Value, a.Currency);
    }

    public static CodalMoney operator -(CodalMoney a, CodalMoney b)
    {
        if (a.Currency != b.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
        }

        return new CodalMoney(a.Value - b.Value, a.Currency);
    }

    public bool GreaterThan(CodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value > other.Value;
    }

    public bool GreaterThanOrEqual(CodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value == other.Value || GreaterThan(other);
    }

    public bool LessThan(CodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value < other.Value;
    }

    public bool LessThanOrEqual(CodalMoney other)
    {
        if (Currency != other.Currency)
        {
            throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
        }

        return Value == other.Value || LessThan(other);
    }

    public static CodalMoney operator *(decimal a, CodalMoney b)
    {
        return new CodalMoney(b.Value * a, b.Currency);
    }

    public static CodalMoney operator *(CodalMoney a, decimal b)
    {
        return new CodalMoney(a.Value * b, a.Currency);
    }

    public static CodalMoney BaseCurrency(decimal amount)
    {
        return new CodalMoney(amount, AppConfig.BASE_CURRENCY);
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

    public static implicit operator decimal(CodalMoney money) => money.Value;

    public static implicit operator CodalMoney(decimal money) => new(money);

    public bool Equals(CodalMoney? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return _value == other._value && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is CodalMoney other && Equals(other));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value, (int)Currency);
    }
}