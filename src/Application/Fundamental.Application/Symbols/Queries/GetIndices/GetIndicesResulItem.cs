using DNTPersianUtils.Core;

namespace Fundamental.Application.Symbols.Queries.GetIndices;

public sealed class GetIndicesResulItem
{
    public decimal Value { get; init; }

    public DateOnly Date { get; set; }

    public string PersianDate => Date.ToShortPersianDateString();

    public decimal Change { get; set; }

    public decimal ChangePercent { get; set; }
}