namespace Fundamental.BuildingBlock.Models;


public readonly struct NumberRange<T>(T from, T to)
    where T : struct, IComparable<T>
{
    public T From { get; } = from;
    public T To { get; } = to;
    public bool IsValid() => From.CompareTo(To) <= 0;

    public bool Contains(T value)
    {
        return value.CompareTo(From) >= 0 && value.CompareTo(To) <= 0;
    }

    public override string ToString()
    {
        return $"{From}..{To}";
    }

    public void Deconstruct(out T from, out T to)
    {
        from = From;
        to = To;
    }
}