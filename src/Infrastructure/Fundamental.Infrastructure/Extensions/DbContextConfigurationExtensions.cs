using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
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
        NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("FundamentalDbContext"));
        dataSourceBuilder.UseNodaTime();
        dataSourceBuilder.MapEnum<IsoCurrency>();
        dataSourceBuilder.MapEnum<ReportingType>();
        dataSourceBuilder.MapEnum<CompanyType>();
        dataSourceBuilder.MapEnum<EnableSubCompany>();
        dataSourceBuilder.MapEnum<PublisherFundType>();
        dataSourceBuilder.MapEnum<PublisherSubCompanyType>();
        dataSourceBuilder.MapEnum<PublisherMarketType>();
        dataSourceBuilder.MapEnum<PublisherState>();
        NpgsqlDataSource dataSource = dataSourceBuilder.Build();

        services.AddDbContext<FundamentalDbContext>(
            options => options.UseNpgsql(
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
                .EnableDetailedErrors());
        return services;
    }
}