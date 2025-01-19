namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

public sealed class GetMonthlyActivitiesResultItem
{
    public Guid Id { get; init; }
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public string Title { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }
    public ushort YearEndMonth { get; init; }
    public ushort ReportMonth { get; init; }
    public decimal SaleBeforeCurrentMonth { get; init; }
    public decimal SaleCurrentMonth { get; init; }
    public decimal SaleIncludeCurrentMonth { get; init; }
    public decimal SaleLastYear { get; init; }
    public bool HasSubCompanySale { get; init; }
    public ulong TraceNo { get; init; }
    public DateTime CreatedAt { get; init; }

    public required DateTime? UpdatedAt { get; init; }
}