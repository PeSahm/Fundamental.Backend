using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Statements.ValueObjects;

public class StatementMonth : ValueObject
{
    public StatementMonth(int month)
    {
        if (month is < 1 or > 12)
        {
            throw new InvalidStatementMonthException(month);
        }

        Month = month;
    }

    public int Month { get; }

    public static implicit operator int(StatementMonth month)
    {
        return month.Month;
    }

    public static implicit operator StatementMonth(int month)
    {
        return new(month);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Month;
    }
}