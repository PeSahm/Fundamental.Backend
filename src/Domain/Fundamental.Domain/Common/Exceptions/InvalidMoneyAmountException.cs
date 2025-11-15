namespace Fundamental.Domain.Common.Exceptions;

public class InvalidMoneyAmountException : Exception
{
    public InvalidMoneyAmountException(decimal amount)
        : base($"Money amount {amount} is out of range.")
    {
    }
}