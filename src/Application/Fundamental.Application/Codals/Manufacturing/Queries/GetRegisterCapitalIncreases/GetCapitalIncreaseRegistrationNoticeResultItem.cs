namespace Fundamental.Application.Codals.Manufacturing.Queries.GetRegisterCapitalIncreases;

public sealed class GetCapitalIncreaseRegistrationNoticeResultItem
{
    public Guid Id { get; init; }
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public string Title { get; init; }
    public string Uri { get; init; }
    public decimal CashIncoming { get; init; }
    public string? LastCapitalIncreaseSession { get; init; }
    public DateOnly? LastExtraAssemblyDate { get; init; }
    public decimal NewCapital { get; init; }
    public decimal PreviousCapital { get; init; }
    public decimal Reserves { get; init; }
    public decimal RetainedEarning { get; init; }
    public decimal RevaluationSurplus { get; init; }
    public decimal SarfSaham { get; init; }
    public DateOnly StartDate { get; init; }
    public long? PrimaryMarketTracingNo { get; init; }
    public decimal CashForceclosurePriority { get; init; }
    public ulong TraceNo { get; init; }
    public DateTime CreatedAt { get; init; }
    public required DateTime? UpdatedAt { get; init; }
}