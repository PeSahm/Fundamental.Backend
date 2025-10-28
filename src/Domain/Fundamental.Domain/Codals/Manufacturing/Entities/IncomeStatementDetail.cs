using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class IncomeStatementDetail : BaseEntity<Guid>
{
    public IncomeStatementDetail(
        Guid id,
        IncomeStatement incomeStatement,
        ushort row,
        ushort codalRow,
        string? description,
        SignedCodalMoney value,
        DateTime createdAt
    )
    {
        Id = id;
        IncomeStatement = incomeStatement;
        Row = row;
        CodalRow = codalRow;
        Description = description;
        Value = value;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected IncomeStatementDetail()
    {
    }

    public IncomeStatement IncomeStatement { get; private set; }

    public ushort Row { get; private set; }

    public ushort CodalRow { get; private set; }

    public string? Description { get; private set; }

    public SignedCodalMoney Value { get; private set; }

    public void UpdateValue(SignedCodalMoney newValue)
    {
        Value = newValue;
        UpdatedAt = DateTime.UtcNow;
    }
}