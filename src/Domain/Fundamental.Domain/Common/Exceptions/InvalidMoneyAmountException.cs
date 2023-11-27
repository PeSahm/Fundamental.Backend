namespace Fundamental.Domain.Common.Exceptions
{
    [Serializable]
    public class InvalidMoneyAmountException : Exception
    {
        public InvalidMoneyAmountException(decimal amount)
            : base($"Money amount {amount} is out of range.")
        {
        }

        protected InvalidMoneyAmountException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}