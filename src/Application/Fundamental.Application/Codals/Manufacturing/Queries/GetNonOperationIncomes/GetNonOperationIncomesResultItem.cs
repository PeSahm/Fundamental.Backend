namespace Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;

public sealed class GetNonOperationIncomesResultItem
{
    public required Guid Id { get; set; }

    public required string Isin { get; set; }

    public required string Symbol { get; init; }

    public required string Title { get; init; }

    public required ulong TraceNo { get; set; }

    public string Uri { get; set; }

    public ushort FiscalYear { get; set; }

    public ushort YearEndMonth { get; set; }

    public ushort ReportMonth { get; set; }

    public string? Description { get; set; }

    public required decimal Value { get; set; }

    public bool IsAudited { get; set; }
}