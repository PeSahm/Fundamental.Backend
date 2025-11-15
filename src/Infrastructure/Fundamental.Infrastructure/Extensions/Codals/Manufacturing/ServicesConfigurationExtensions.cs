using Fundamental.Application.Codals.Dto.MonthlyActivities.V1;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V2;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V3;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;
using Fundamental.Application.Codals.Manufacturing.EventHandlers;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Infrastructure.Repositories;
using Fundamental.Infrastructure.Repositories.Codals.Manufacturing;
using Fundamental.Infrastructure.Services;
using Fundamental.Infrastructure.Services.Codals.Factories;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.IncomeStatements;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages4;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages5;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.RegisterCapitalIncrease;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions.Codals.Manufacturing;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection AddManufacturingCodalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCodalServiceDetectorServices()
            .AddCodalProcessorServices()
            .AddCodalMonthlyActivityMappingServices();
        return serviceCollection;
    }

    public static IServiceCollection AddCodalMonthlyActivityMappingServices(this IServiceCollection serviceCollection)
    {
        // Register canonical mapping services
        serviceCollection.AddCanonicalMappingServiceFactory();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV1>,
                MonthlyActivityMappingServiceV1, CodalMonthlyActivityV1>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV2>,
                MonthlyActivityMappingServiceV2, CodalMonthlyActivityV2>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV3>,
                MonthlyActivityMappingServiceV3, CodalMonthlyActivityV3>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivity>,
                MonthlyActivityMappingServiceV4, CodalMonthlyActivity>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>,
                MonthlyActivityMappingServiceV5, CodalMonthlyActivityV5>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalInterpretativeReportSummaryPage5, CodalInterpretativeReportSummaryPage5V2>,
                InterpretativeReportSummaryPage5MappingServiceV2, CodalInterpretativeReportSummaryPage5V2>();
        return serviceCollection;
    }

    public static IServiceCollection AddCodalProcessorServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV1Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV2Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV3Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV4Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV5Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, BalanceSheetV5Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, IncomeStatementsV7Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, NonOperationIncomeAndExpensesV2Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, TheStatusOfViableCompaniesV2Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, CapitalIncreaseRegistrationNoticeV1Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, InterpretativeReportSummaryPage5V2Processor>();
        return serviceCollection;
    }

    public static IServiceCollection AddCodalServiceDetectorServices(this IServiceCollection services)
    {
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, MonthlyActivityDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, BalanceSheetDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, IncomeStatementDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, NonOperationIncomeAndExpensesDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, TheStatusOfViableCompaniesDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, CapitalIncreaseRegistrationNoticeDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, InterpretativeReportSummaryPage5Detector>();
        return services;
    }

    public static IServiceCollection AddManufacturingReadRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMonthlyActivityRepository, MonthlyActivityRepository>();
        serviceCollection.AddScoped<IBalanceSheetReadRepository, BalanceSheetReadRepository>();
        serviceCollection.AddScoped<IIncomeStatementsReadRepository, IncomeStatementReadRepository>();
        serviceCollection.AddScoped<INonOperationIncomesRepository, NonOperationIncomesRepository>();
        serviceCollection.AddScoped<IStatusOfViableCompaniesRepository, StatusOfViableCompaniesRepository>();
        serviceCollection.AddScoped<IIndicesRepository, IndicesRepository>();
        serviceCollection.AddScoped<IFinancialStatementReadRepository, FinancialStatementReadRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddManufacturingEventDispatcher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<EventDataDispatcher>();
        return serviceCollection;
    }
}