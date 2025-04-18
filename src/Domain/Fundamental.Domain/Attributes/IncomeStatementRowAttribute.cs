namespace Fundamental.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class IncomeStatementRowAttribute(ushort row) : Attribute
{
    public ushort Row { get; } = row;
}