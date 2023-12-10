using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Statements.ValueObjects;

public class StatementMonth : ValueObject
{
    public StatementMonth(ushort month)
    {
        if (month is < 1 or > 12)
        {
            throw new InvalidStatementMonthException(month);
        }

        Month = month;
    }

    public StatementMonth(int month)
    {
        if (month is < 1 or > 12)
        {
            throw new InvalidStatementMonthException(month);
        }

        Month = (ushort)month;
    }

    public ushort Month { get; }

    public static implicit operator ushort(StatementMonth month)
    {
        return month.Month;
    }

    public static implicit operator StatementMonth(ushort month)
    {
        return new StatementMonth(month);
    }

    public static implicit operator StatementMonth(int month)
    {
        return new StatementMonth(month);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Month;
    }
}