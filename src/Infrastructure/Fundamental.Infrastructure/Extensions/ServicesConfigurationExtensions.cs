using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Common.PipelineBehaviors;
using Fundamental.Application.Common.Validators;
using Fundamental.Application.Services;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Application.Utilities.Services;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Infrastructure.Extensions.Codals.Manufacturing;
using Fundamental.Infrastructure.HostedServices.Codals;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Persistence.Repositories.Base;
using Fundamental.Infrastructure.Repositories;
using Fundamental.Infrastructure.Services;
using Fundamental.Infrastructure.Services.Codals;
using Fundamental.Infrastructure.Services.Codals.Factories;
using Fundamental.Infrastructure.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions;

public static class ServicesConfigurationExtensions
{
    public static void AddCodalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICodalVersionDetectorFactory, CodalVersionDetectorFactory>();
        serviceCollection.AddScoped<ICodalProcessorFactory, CodalProcessorFactory>();
        serviceCollection.AddManufacturingCodalServices();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(FundamentalRepository<>));
        builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetService<FundamentalDbContext>()!);
        builder.Services.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));
        builder.Services.AddScoped<ICustomerErrorMessagesService, CustomerErrorMessagesService>();
        builder.Services.AddScoped<ICodalService, CodalService>();
        builder.Services.AddScoped<IMarketDataService, MarketDataService>();

        builder.Services.AddSingleton<IIpService, IpService>();

        builder.Services.AddMediatR(
            cfg =>
            {
                cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
                cfg.RegisterServicesFromAssemblies(typeof(GetSymbolsQueryHandler).Assembly);
            });
    }

    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddPartialOptions<MdpOption>("Mdp", builder.Configuration);
    }

    public static void AddReadRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISymbolRelationRepository, SymbolRelationRepository>();
        builder.AddManufacturingReadRepositories();
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

    public static void AddHostedServices(this IServiceCollection builder)
    {
        builder.AddHostedService<CommonCodalDataHostedService>();
        builder.AddManufacturingHostedServices();
    }
}