using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Prices.Entities;

public class ClosePrice : BaseEntity<Guid>
{
    public ClosePrice(Guid id, Symbol symbol, DateTime date)
    {
        Id = id;
        Symbol = symbol;
        Date = date;
    }

    protected ClosePrice()
    {
    }

    public Symbol Symbol { get; private set; }
    public DateTime Date { get; private set; }

    public ulong Close { get; private set; }

    public ulong Open { get; private set; }

    public ulong High { get; private set; }

    public ulong Low { get; private set; }

    public ulong Last { get; private set; }

    public ulong CloseAdjusted { get; private set; }

    public ulong OpenAdjusted { get; private set; }

    public ulong HighAdjusted { get; private set; }

    public ulong LowAdjusted { get; private set; }

    public ulong LastAdjusted { get; private set; }

    public ulong Volume { get; private set; }

    public ulong Quantity { get; private set; }

    public ulong Value { get; private set; }

    public void SetPrice(ulong close, ulong open, ulong high, ulong low, ulong last)
    {
        Close = close;
        Open = open;
        High = high;
        Low = low;
        Last = last;
    }

    public void SetAdjustedPrice(ulong closeAdjusted, ulong openAdjusted, ulong highAdjusted, ulong lowAdjusted, ulong lastAdjusted)
    {
        CloseAdjusted = closeAdjusted;
        OpenAdjusted = openAdjusted;
        HighAdjusted = highAdjusted;
        LowAdjusted = lowAdjusted;
        LastAdjusted = lastAdjusted;
    }

    public void SetPriceStatistics(ulong volume, ulong quantity, ulong value)
    {
        Volume = volume;
        Quantity = quantity;
        Value = value;
    }
}