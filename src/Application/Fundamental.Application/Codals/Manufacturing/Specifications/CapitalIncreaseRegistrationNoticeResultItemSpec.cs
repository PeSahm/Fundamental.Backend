using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetRegisterCapitalIncreases;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class CapitalIncreaseRegistrationNoticeResultItemSpec : Specification<CapitalIncreaseRegistrationNotice, GetCapitalIncreaseRegistrationNoticeResultItem>
{
    public CapitalIncreaseRegistrationNoticeResultItemSpec()
    {
        Query.Select(x => new GetCapitalIncreaseRegistrationNoticeResultItem
        {
            Id = x.Id,
            Isin = x.Symbol.Isin,
            Symbol = x.Symbol.Name,
            Title = x.Symbol.Title,
            Uri = x.Uri,
            CashIncoming = x.CashIncoming.Value,
            LastExtraAssemblyDate = x.LastExtraAssemblyDate,
            NewCapital = x.NewCapital.Value,
            PreviousCapital = x.PreviousCapital.Value,
            Reserves = x.Reserves.Value,
            RetainedEarning = x.RetainedEarning.Value,
            RevaluationSurplus = x.RevaluationSurplus.Value,
            SarfSaham = x.SarfSaham.Value,
            StartDate = x.StartDate,
            PrimaryMarketTracingNo = x.PrimaryMarketTracingNo,
            CashForceclosurePriority = x.CashForceclosurePriority,
            TraceNo = x.TraceNo,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        });
    }
}