using Fundamental.Application.Codals.Dto.MonthlyActivities.V1;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V2;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V3;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
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
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.RegisterCapitalIncrease;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Extensions.Codals.Manufacturing;

public static class ServicesConfigurationExtensions
{
    public static void AddManufacturingCodalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, MonthlyActivityDetector>();
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, BalanceSheetDetector>();
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, IncomeStatementDetector>();
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, NonOperationIncomeAndExpensesDetector>();
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, TheStatusOfViableCompaniesDetector>();
        serviceCollection.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, CapitalIncreaseRegistrationNoticeDetector>();

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
    }

    public static void AddManufacturingReadRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMonthlyActivityRepository, MonthlyActivityRepository>();
        builder.Services.AddScoped<IBalanceSheetReadRepository, BalanceSheetReadRepository>();
        builder.Services.AddScoped<IIncomeStatementsReadRepository, IncomeStatementReadRepository>();
        builder.Services.AddScoped<INonOperationIncomesRepository, NonOperationIncomesRepository>();
        builder.Services.AddScoped<IStatusOfViableCompaniesRepository, StatusOfViableCompaniesRepository>();
        builder.Services.AddScoped<IIndicesRepository, IndicesRepository>();
        builder.Services.AddScoped<IFinancialStatementReadRepository, FinancialStatementReadRepository>();
    }

    public static void AddManufacturingEventDispatcher(this IServiceCollection builder)
    {
        builder.AddScoped<EventDataDispatcher>();
    }
}