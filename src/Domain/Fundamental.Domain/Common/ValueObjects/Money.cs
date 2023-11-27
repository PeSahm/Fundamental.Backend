using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Common.ValueObjects
{
    public record Money
    {
        public Money(decimal amount, IsoCurrency currency)
        {
            if (amount < 0)
            {
                throw new InvalidMoneyAmountException(amount);
            }

            Value = amount;
            Currency = currency;
        }

        protected Money()
        {
        }

        public decimal Value { get; }
        public IsoCurrency Currency { get; }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
            }

            return new Money(a.Value + b.Value, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(a.Currency, b.Currency);
            }

            return new Money(a.Value - b.Value, a.Currency);
        }

        public bool GreaterThan(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
            }

            return Value > other.Value;
        }

        public bool GreaterThanOrEqual(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
            }

            return Value == other.Value || GreaterThan(other);
        }

        public bool LessThan(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
            }

            return Value < other.Value;
        }

        public bool LessThanOrEqual(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new MoneyCurrencyIsIncompatibleException(Currency, other.Currency);
            }

            return Value == other.Value || LessThan(other);
        }

        public static Money operator *(decimal a, Money b)
        {
            return new Money(b.Value * a, b.Currency);
        }

        public static Money operator *(Money a, decimal b)
        {
            return new Money(a.Value * b, a.Currency);
        }

        public static Money BaseCurrency(decimal amount)
        {
            return new Money(amount, AppConfig.BASE_CURRENCY);
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
    }
}