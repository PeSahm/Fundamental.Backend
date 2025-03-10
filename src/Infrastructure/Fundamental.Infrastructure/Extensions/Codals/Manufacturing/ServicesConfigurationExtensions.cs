using Fundamental.Application.Codals.Manufacturing.EventHandlers;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Infrastructure.HostedServices.Codals.Manufacturing;
using Fundamental.Infrastructure.Repositories;
using Fundamental.Infrastructure.Repositories.Codals.Manufacturing;
using Fundamental.Infrastructure.Services;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.IncomeStatements;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages4;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages5;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;
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

        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, MonthlyActivityV4Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, BalanceSheetV5Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, IncomeStatementsV7Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, NonOperationIncomeAndExpensesV2Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, TheStatusOfViableCompaniesV2Processor>();
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

    public static void AddManufacturingHostedServices(this IServiceCollection builder)
    {
        builder.AddHostedService<CodalHostedService>();
        builder.AddHostedService<CalculationHostedService>();
    }
}