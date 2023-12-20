namespace Fundamental.Application.Statements.Queries.GetMonthlyActivities;

public class GetMonthlyActivitiesResultItem
{
    public Guid Id { get; set; }
    public string Isin { get; set; }
    public string Symbol { get; set; }
    public string Title { get; set; }
    public string Uri { get; set; }
    public ushort FiscalYear { get; set; }
    public ushort YearEndMonth { get; set; }
    public ushort ReportMonth { get; set; }
    public decimal SaleBeforeCurrentMonth { get; set; }
    public decimal SaleCurrentMonth { get; set; }
    public decimal SaleIncludeCurrentMonth { get; set; }
    public decimal SaleLastYear { get; set; }
    public bool HasSubCompanySale { get; set; }
    public ulong TraceNo { get; set; }
    public DateTime CreatedAt { get; set; }
}