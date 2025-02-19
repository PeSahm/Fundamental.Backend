using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Codals.ValueObjects;

public class StatementMonth : ValueObject
{
    private bool IsEmpty = false;

    public static StatementMonth Empty => new StatementMonth();
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

    private StatementMonth()
    {
        IsEmpty = true;
    }

    public ushort Month { get; }

    public StatementMonth AdjustedMonth(StatementMonth yearEndMonth)
    {
        int adjustedMonthValue = (this + (12 - yearEndMonth)) % 12;

        if (adjustedMonthValue == 0)
        {
            adjustedMonthValue = 12;
        }

        return new StatementMonth(adjustedMonthValue);
    }

    public bool IsEmptyStatementMonth()
    {
        return IsEmpty;
    }

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