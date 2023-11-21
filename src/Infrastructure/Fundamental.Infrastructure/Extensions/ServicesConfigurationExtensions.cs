using Fundamental.Domain.Repositories.Base;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class ServicesConfigurationExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(FundamentalRepository<>));
        builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetService<FundamentalDbContext>()!);

        builder.Services.AddMediatR(
            _ =>
            {
                // cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
                // cfg.RegisterServicesFromAssemblies(WifiApplicationRootMarker.Assembly);
            });
    }
}