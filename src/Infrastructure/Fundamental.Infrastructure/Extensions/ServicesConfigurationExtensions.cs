using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Common.PipelineBehaviors;
using Fundamental.Application.Common.Validators;
using Fundamental.Application.Services;
using Fundamental.Application.Symbols;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Application.Utilities.Services;
using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Infrastructure.Extensions.Codals.Manufacturing;
using Fundamental.Infrastructure.HostedServices.Codals;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Persistence.Repositories.Base;
using Fundamental.Infrastructure.Repositories;
using Fundamental.Infrastructure.Services;
using Fundamental.Infrastructure.Services.Codals;
using Fundamental.Infrastructure.Services.Codals.Factories;
using Fundamental.Infrastructure.Services.Symbols;
using Fundamental.Infrastructure.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Savorboard.CAP.InMemoryMessageQueue;

namespace Fundamental.Infrastructure.Extensions;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection AddCodalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICodalVersionDetectorFactory, CodalVersionDetectorFactory>();
        serviceCollection.AddScoped<ICodalProcessorFactory, CodalProcessorFactory>();
        serviceCollection.AddManufacturingCodalServices();
        return serviceCollection;
    }

    public static IServiceCollection AddEventDispatcher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddManufacturingEventDispatcher();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection builder)
    {
        builder.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<FundamentalDbContext>());
        builder.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));
        builder.AddScoped<ICustomerErrorMessagesService, CustomerErrorMessagesService>();
        builder.AddScoped<ICodalService, CodalService>();
        builder.AddScoped<IMarketDataService, MarketDataService>();
        builder.AddScoped<IShareHoldersService, ShareHoldersService>();

        builder.AddSingleton<IIpService, IpService>();

        builder.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(LogRequestsPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
            cfg.RegisterServicesFromAssemblies(typeof(GetSymbolsQueryHandler).Assembly);
        });
        return builder;
    }

    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddPartialOptions<MdpOption>("Mdp", builder.Configuration);
        builder.Services.AddPartialOptions<TseTmcOption>("TseTmc", builder.Configuration);
    }

    public static IServiceCollection AddReadRepositories(this IServiceCollection builder)
    {
        builder.AddScoped<IRepository, FundamentalRepository>();
        builder.AddScoped<ISymbolRelationRepository, SymbolRelationRepository>();
        builder.AddScoped<ISymbolShareHoldersReadRepository, SymbolShareHoldersReadRepository>();
        builder.AddManufacturingReadRepositories();
        return builder;
    }

    public static IServiceCollection AddPartialOptions<TOptions>(
        this IServiceCollection services,
        string section,
        IConfiguration configuration
    )
        where TOptions : class
    {
        services.Configure<TOptions>(configuration.GetSection(section));
        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection builder)
    {
        builder.AddHostedService<CommonCodalDataHostedService>();
        return builder;
    }

    public static IServiceCollection AddBuilders(this IServiceCollection builder)
    {
        builder.AddSingleton<IFinancialStatementBuilder, FinancialStatementBuilder>();
        return builder;
    }

    public static IServiceCollection AddCap(this IServiceCollection builder)
    {
        builder.AddCap(options =>
        {
            options.UseDashboard(dashboardOptions =>
            {
            });
            options.UseInMemoryMessageQueue();
            options.UseEntityFramework<FundamentalDbContext>(efOptions =>
            {
                efOptions.Schema = "cap";
            });
            options.Version = "1";
            options.FailedThresholdCallback = async void (failedInfo) =>
            {
                ILogger<ServiceProvider>? logger = failedInfo.ServiceProvider.GetService<ILogger<ServiceProvider>>();
                logger?.LogError(
                    "Failed message threshold reached. Message: {Message}, MessageType: {MessageType}",
                    failedInfo.Message,
                    failedInfo.MessageType
                );
                await Task.CompletedTask;
            };
            options.UseStorageLock = true;
            options.ConsumerThreadCount = 2;

            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        return builder;
    }
}