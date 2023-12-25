using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Codals.ValueObjects;

public class FiscalYear : ValueObject
{
    public FiscalYear(ushort year)
    {
        Year = year;
    }

    public FiscalYear(int year)
    {
        Year = (ushort)year;
    }

    protected FiscalYear()
    {
    }

    public ushort Year { get; }

    public static implicit operator FiscalYear(ushort year)
    {
        return new FiscalYear(year);
    }

    public static implicit operator FiscalYear(int year)
    {
        return new FiscalYear(year);
    }

    public static implicit operator ushort(FiscalYear year)
    {
        return year.Year;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
    }
}