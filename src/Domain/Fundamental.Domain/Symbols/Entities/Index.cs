using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.Symbols.Entities;

public class Index : BaseEntity<Guid>
{
    public Index(
        Guid id,
        Symbol symbol,
        DateOnly date,
        decimal open,
        decimal high,
        decimal low,
        decimal value,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Value = value;
        CreatedAt = createdAt;
    }

    private Index()
    {
    }

    public Symbol Symbol { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Open { get; private set; }
    public decimal High { get; private set; }
    public decimal Low { get; private set; }
    public decimal Value { get; private set; }

    public decimal GetChangePercentage(Index previousIndex)
    {
        return GetChange(previousIndex) / previousIndex.Value * 100;
    }

    public decimal GetChange(Index previousIndex)
    {
        return Value - previousIndex.Value;
    }

    public void UpdateIndex(
        decimal open,
        decimal high,
        decimal low,
        decimal value
    )
    {
        Open = open;
        High = high;
        Low = low;
        Value = value;
        UpdatedAt = DateTime.Now;
    }
}