using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Statements.ValueObjects;

public class FiscalYear : ValueObject
{
    public int Year { get; }

    public FiscalYear(int year)
    {
        Year = year;
    }

    protected FiscalYear()
    {
    }

    public static implicit operator FiscalYear(int year)
    {
        return new FiscalYear(year);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
    }
}