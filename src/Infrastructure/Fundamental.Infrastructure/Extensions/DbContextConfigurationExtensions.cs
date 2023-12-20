using Fundamental.Infrastructure.Persistence;
using Gridify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class DbContextConfigurationExtensions
{
    private const string MIGRATIONS_ASSEMBLY = "Migrations";

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        GridifyGlobalConfiguration.EnableEntityFrameworkCompatibilityLayer();
        services.AddDbContext<FundamentalDbContext>(
            options => options.UseSqlServer(
                    configuration.GetConnectionString("FundamentalDbContext"),
                    b =>
                        b.EnableRetryOnFailure()
                            .MigrationsAssembly(MIGRATIONS_ASSEMBLY)
                )
#if DEBUG
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
#endif

        return services;
    }
}