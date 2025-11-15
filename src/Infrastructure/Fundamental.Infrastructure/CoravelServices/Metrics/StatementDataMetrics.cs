using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.CoravelServices.Metrics;

public class StatementDataMetrics(IServiceScopeFactory factory) : Coravel.Pro.Features.Metrics.IMetricCard
{
    public async Task ExecuteAsync()
    {
        IServiceScope scope = factory.CreateScope();
        FundamentalDbContext database = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        DateTime today = DateTime.UtcNow.Date;

        int balanseSheetCount = await database.BalanceSheets
            .Where(x => x.CreatedAt.Date == today)
            .CountAsync();

        Value = balanseSheetCount.ToString() ?? "0";
        Title = "صورت وضعیت مالی";
        SubTitle = "تعداد صورت وضعیت مالی امروز";
        IsPositiveMetric = balanseSheetCount > 0;
        IncludeArrow = false;
    }

    public string Value { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public bool IsPositiveMetric { get; set; }
    public bool IncludeArrow { get; set; }
}