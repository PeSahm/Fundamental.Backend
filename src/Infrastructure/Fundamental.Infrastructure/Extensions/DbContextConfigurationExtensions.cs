using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class DbContextConfigurationExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FundamentalDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("FundamentalDbConnection"),
                b => b.EnableRetryOnFailure())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
        return services;
    }
}