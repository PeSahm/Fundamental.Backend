namespace Fundamental.Domain.Attributes;

public sealed class IncomeStatementRowAttribute(ushort row) : Attribute
{
    public ushort Row { get; } = row;
}