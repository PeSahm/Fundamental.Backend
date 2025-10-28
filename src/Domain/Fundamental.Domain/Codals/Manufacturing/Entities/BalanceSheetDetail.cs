using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class BalanceSheetDetail : BaseEntity<Guid>
{
    public BalanceSheetDetail(
        Guid id,
        BalanceSheet balanceSheet,
        ushort row,
        ushort codalRow,
        BalanceSheetCategory codalCategory,
        string? description,
        SignedCodalMoney value,
        DateTime createdAt
    )
    {
        Id = id;
        BalanceSheet = balanceSheet;
        Row = row;
        CodalRow = codalRow;
        CodalCategory = codalCategory;
        Description = description;
        Value = value;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected BalanceSheetDetail()
    {
    }

    public BalanceSheet BalanceSheet { get; private set; }

    public ushort Row { get; private set; }

    public ushort CodalRow { get; private set; }

    public BalanceSheetCategory CodalCategory { get; private set; }

    public string? Description { get; private set; }

    public SignedCodalMoney Value { get; private set; }
}