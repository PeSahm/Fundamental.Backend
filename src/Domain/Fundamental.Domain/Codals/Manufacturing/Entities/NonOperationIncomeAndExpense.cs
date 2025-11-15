using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public sealed class NonOperationIncomeAndExpense : BaseEntity<Guid>
{
    public NonOperationIncomeAndExpense(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        string? description,
        SignedCodalMoney value,
        bool isAudited,
        bool currentPeriod,
        bool previousPeriod,
        bool forecastPeriod,
        bool yearlyForecastPeriod,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        Description = description;
        Value = value;
        IsAudited = isAudited;
        CurrentPeriod = currentPeriod;
        PreviousPeriod = previousPeriod;
        ForecastPeriod = forecastPeriod;
        YearlyForecastPeriod = yearlyForecastPeriod;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    private NonOperationIncomeAndExpense()
    {
    }

    public Symbol Symbol { get; private set; }

    public ulong TraceNo { get; private set; }

    public string Uri { get; private set; }

    public FiscalYear FiscalYear { get; private set; }

    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    public StatementMonth YearEndMonth { get; private set; }

    public StatementMonth ReportMonth { get; private set; }

    public string? Description { get; private set; }

    public SignedCodalMoney Value { get; private set; }

    public bool IsAudited { get; private set; }

    public bool CurrentPeriod { get; private set; }

    public bool PreviousPeriod { get; private set; }

    public bool ForecastPeriod { get; private set; }

    public bool YearlyForecastPeriod { get; set; }

    private readonly List<NoneOperationalIncomeTag> _tags = new();

    public NonOperationIncomeAndExpense UpdateTags(NoneOperationalIncomeTag[] tags)
    {
        _tags.Clear();
        _tags.AddRange(tags);
        return this;
    }

    public List<NoneOperationalIncomeTag> Tags => _tags;
}