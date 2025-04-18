namespace Fundamental.Domain.Common.BaseTypes;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        ValueObject valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1,
                (current, obj) =>
                {
                    unchecked
                    {
                        return (current * 23) + obj.GetHashCode();
                    }
                });
    }

    protected abstract IEnumerable<object> GetEqualityComponents();
}