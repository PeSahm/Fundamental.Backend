using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Persistence.Interceptors;
using Gridify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class DbContextConfigurationExtensions
{
    private const string MIGRATIONS_ASSEMBLY = "Migrations";

    /// <summary>
    /// Registers FundamentalDbContext in the dependency injection container and configures its PostgreSQL provider and EF Core behavior.
    /// </summary>
    /// <returns>The same <see cref="IServiceCollection"/> instance to allow fluent chaining.</returns>
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        GridifyGlobalConfiguration.EnableEntityFrameworkCompatibilityLayer();

        services.AddDbContext<FundamentalDbContext>((sp, options) =>
        {
            DomainEventsInterceptor? domainEventsInterceptor = sp.GetService<DomainEventsInterceptor>();

            if (domainEventsInterceptor is not null)
            {
               // options.AddInterceptors(sp.GetRequiredService<DomainEventsInterceptor>());
            }

            options.UseNpgsql(
                    configuration.GetConnectionString("FundamentalDbContext"),
                    b =>
                        b.EnableRetryOnFailure()
                            .SetPostgresVersion(16, 1)
                            .MigrationsAssembly(MIGRATIONS_ASSEMBLY)
                            .UseNodaTime()
                            .MapEnum<IsoCurrency>()
                            .MapEnum<ReportingType>()
                            .MapEnum<CompanyType>()
                            .MapEnum<EnableSubCompany>()
                            .MapEnum<PublisherFundType>()
                            .MapEnum<PublisherSubCompanyType>()
                            .MapEnum<PublisherMarketType>()
                            .MapEnum<PublisherState>()
                            .MapEnum<ReviewStatus>()
                            .MapEnum<ProductType>()
                            .MapEnum<ExchangeType>()
                            .MapEnum<EtfType>()
                            .MapEnum<NoneOperationalIncomeTag>()
                            .MapEnum<CodalVersion>()
                )
                .UseSnakeCaseNamingConvention()
#if DEBUG
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
#endif
                .EnableDetailedErrors();
        });
        return services;
    }

    public static IServiceCollection AddInterceptors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DomainEventsInterceptor>();
        return serviceCollection;
    }
}