using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Persistence.Interceptors;
using Gridify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Fundamental.Infrastructure.Extensions;

public static class DbContextConfigurationExtensions
{
    private const string MIGRATIONS_ASSEMBLY = "Migrations";

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        GridifyGlobalConfiguration.EnableEntityFrameworkCompatibilityLayer();
        NpgsqlDataSourceBuilder dataSourceBuilder = new(configuration.GetConnectionString("FundamentalDbContext"));
        dataSourceBuilder.UseNodaTime();
        dataSourceBuilder.MapEnum<IsoCurrency>();
        dataSourceBuilder.MapEnum<ReportingType>();
        dataSourceBuilder.MapEnum<CompanyType>();
        dataSourceBuilder.MapEnum<EnableSubCompany>();
        dataSourceBuilder.MapEnum<PublisherFundType>();
        dataSourceBuilder.MapEnum<PublisherSubCompanyType>();
        dataSourceBuilder.MapEnum<PublisherMarketType>();
        dataSourceBuilder.MapEnum<PublisherState>();
        dataSourceBuilder.MapEnum<ReviewStatus>();
        dataSourceBuilder.MapEnum<ProductType>();
        dataSourceBuilder.MapEnum<ExchangeType>();
        dataSourceBuilder.MapEnum<EtfType>();
        dataSourceBuilder.MapEnum<NoneOperationalIncomeTag>();

        NpgsqlDataSource dataSource = dataSourceBuilder.Build();

        services.AddDbContext<FundamentalDbContext>((sp, options) =>
        {
            DomainEventsInterceptor? domainEventsInterceptor = sp.GetService<DomainEventsInterceptor>();

            if (domainEventsInterceptor is not null)
            {
                options.AddInterceptors(sp.GetRequiredService<DomainEventsInterceptor>());
            }

            options.UseNpgsql(
                    dataSource,
                    b =>
                        b.EnableRetryOnFailure()
                            .MigrationsAssembly(MIGRATIONS_ASSEMBLY)
                            .UseNodaTime()
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