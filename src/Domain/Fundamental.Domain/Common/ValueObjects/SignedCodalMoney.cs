using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Common.ValueObjects;

public sealed class SignedCodalMoney : IEquatable<SignedCodalMoney>
{
    public SignedCodalMoney(decimal amount, IsoCurrency currency)
    {
        RealValue = amount * CodalMoneyMultiplier;
        Currency = currency;
    }

    public SignedCodalMoney(decimal amount)
    {
        RealValue = amount * CodalMoneyMultiplier;
        Currency = AppConfig.BASE_CURRENCY;
    }

    private SignedCodalMoney()
    {
        SetInternally = true;
        Currency = AppConfig.BASE_CURRENCY;
    }

    public static SignedCodalMoney Empty => new();

    public static decimal CodalMoneyMultiplier => 1_000_000;
    public decimal Value => RealValue / CodalMoneyMultiplier;

    public decimal RealValue { get; }

    public IsoCurrency Currency { get; }

    private bool SetInternally { get; set; }

    public bool Equals(SignedCodalMoney? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return RealValue == other.RealValue && Currency == other.Currency;
    }

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
    ///     Mix money value and currency.
    /// </summary>
    /// <param name="places">Number of decimal places.</param>
    /// <returns>A string object with money format.</returns>
    public string ToString(short places)
    {
        return $"{Value.ToString($"F{places}")} {Currency.ToString()}";
    }

    public static implicit operator decimal(SignedCodalMoney money)
    {
        return money.Value;
    }

    public static implicit operator SignedCodalMoney(decimal money)
    {
        return new SignedCodalMoney(money);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((SignedCodalMoney)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RealValue, (int)Currency);
    }
}