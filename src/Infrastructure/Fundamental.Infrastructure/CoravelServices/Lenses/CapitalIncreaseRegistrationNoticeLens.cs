using Fundamental.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.CoravelServices.Lenses;

public class CapitalIncreaseRegistrationNoticeLens(IServiceScopeFactory factory) : Coravel.Pro.Features.Lenses.Interfaces.ILense
{
    public IQueryable<object> Select(string filter)
    {
        IServiceScope scope = factory.CreateScope();
        FundamentalDbContext database = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        IQueryable<object> query = database.CapitalIncreaseRegistrationNotices
            .Select(x => new
            {
                x.Symbol.Name,
                x.NewCapital,
                x.PreviousCapital,
                x.StartDate,
                x.TraceNo,
                x.CreatedAt
            })
            .Where(i =>
                string.IsNullOrEmpty(filter) || i.Name.Contains(filter)
            ).OrderByDescending(x => x.CreatedAt);

        return query;
    }

    public string Name => "Capital Increase Registration";
}