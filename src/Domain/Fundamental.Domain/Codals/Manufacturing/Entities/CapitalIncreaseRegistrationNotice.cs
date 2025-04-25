using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class CapitalIncreaseRegistrationNotice : BaseEntity<Guid>
{
    public CapitalIncreaseRegistrationNotice(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        DateOnly startDate,
        DateOnly lastExtraAssemblyDate,
        CodalMoney newCapital,
        CodalMoney previousCapital,
        CodalMoney reserves,
        CodalMoney retainedEarning,
        CodalMoney revaluationSurplus,
        CodalMoney cashIncoming,
        CodalMoney sarfSaham,
        CodalMoney cashForceCosurePriority,
        ulong? primaryMarketTracingNo,
        DateTime createdAt
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        StartDate = startDate;
        LastExtraAssemblyDate = lastExtraAssemblyDate;
        NewCapital = newCapital;
        PreviousCapital = previousCapital;
        Reserves = reserves;
        RetainedEarning = retainedEarning;
        RevaluationSurplus = revaluationSurplus;
        CashIncoming = cashIncoming;
        SarfSaham = sarfSaham;
        PrimaryMarketTracingNo = primaryMarketTracingNo;
        CashForceclosurePriority = cashForceCosurePriority;
        CreatedAt = createdAt.ToUniversalTime();
        UpdatedAt = createdAt.ToUniversalTime();

        AddDomainEvent(
            new CapitalIncreaseRegistrationNoticeCreated(
                Symbol.Isin,
                TraceNo,
                Uri,
                StartDate,
                LastExtraAssemblyDate,
                NewCapital.RealValue,
                PreviousCapital.RealValue),
            EventsAddress.CapitalIncrease.CAPITAL_INCREASE_NOTICE_UPDATE);
    }

    protected CapitalIncreaseRegistrationNotice()
    {
    }

    public Symbol Symbol { get; private set; }
    public ulong TraceNo { get; private set; }
    public string Uri { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly LastExtraAssemblyDate { get; private set; }
    public CodalMoney NewCapital { get; private set; }
    public CodalMoney PreviousCapital { get; private set; }
    public CodalMoney Reserves { get; private set; }
    public CodalMoney RetainedEarning { get; private set; }
    public CodalMoney RevaluationSurplus { get; private set; }
    public CodalMoney CashIncoming { get; private set; }
    public CodalMoney SarfSaham { get; private set; }
    public ulong? PrimaryMarketTracingNo { get; private set; }
    public CodalMoney CashForceclosurePriority { get; private set; }
    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;
}