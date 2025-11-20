using Fundamental.Application.Codals.Dto.AnnualAssembly.V1;
using Fundamental.Application.Codals.Dto.ExtraAnnualAssembly.V1;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    InterpretativeReportSummaryPage5;
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
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.AnnualAssembly;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAssembly;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAnnualAssembly;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.IncomeStatements;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages4;
using Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages5;
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

    /// <summary>
    /// Registers canonical mapping infrastructure and keyed scoped canonical mappings used by manufacturing codals.
    /// </summary>
    /// <returns>The updated <see cref="IServiceCollection"/> with canonical mapping services and keyed mappings for monthly activity, interpretative report summary page 5, annual assembly, and extra annual assembly.</returns>
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
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalInterpretativeReportSummaryPage5,
                    CodalInterpretativeReportSummaryPage5V2>,
                InterpretativeReportSummaryPage5MappingServiceV2, CodalInterpretativeReportSummaryPage5V2>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalAnnualAssembly, CodalAnnualAssemblyV1>,
                AnnualAssemblyMappingServiceV1, CodalAnnualAssemblyV1>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalExtraAnnualAssembly, CodalExtraAnnualAssemblyV1>,
                ExtraAnnualAssemblyMappingServiceV1, CodalExtraAnnualAssemblyV1>();
        serviceCollection
            .AddKeyedScopedCanonicalMappingService<ICanonicalMappingService<CanonicalExtraAssembly, CodalExtraAssemblyV1>,
                ExtraAssemblyMappingServiceV1, CodalExtraAssemblyV1>();
        return serviceCollection;
    }

    /// <summary>
    /// Registers keyed scoped CODAL processor implementations used by the manufacturing pipeline.
    /// </summary>
    /// <returns>The <see cref="IServiceCollection"/> after registering the CODAL processors.</returns>
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
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, AnnualAssemblyV1Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, ExtraAnnualAssemblyV1Processor>();
        serviceCollection.AddKeyedScopedCodalProcessor<ICodalProcessor, ExtraAssemblyV1Processor>();
        return serviceCollection;
    }

    /// <summary>
    /// Adds keyed scoped codal version detector implementations for manufacturing codals to the service collection.
    /// </summary>
    /// <returns>The original IServiceCollection with the codal version detector services registered.</returns>
    public static IServiceCollection AddCodalServiceDetectorServices(this IServiceCollection services)
    {
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, MonthlyActivityDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, BalanceSheetDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, IncomeStatementDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, NonOperationIncomeAndExpensesDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, TheStatusOfViableCompaniesDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, CapitalIncreaseRegistrationNoticeDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, InterpretativeReportSummaryPage5Detector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, AnnualAssemblyDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, ExtraAssemblyDetector>();
        services.AddKeyedScopedCodalVersionDetector<ICodalVersionDetector, ExtraAnnualAssemblyDetector>();
        return services;
    }

    /// <summary>
    /// Registers scoped implementations of manufacturing read repositories into the provided service collection.
    /// </summary>
    /// <returns>The same IServiceCollection instance with the manufacturing read repositories registered.</returns>
    public static IServiceCollection AddManufacturingReadRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMonthlyActivityRepository, MonthlyActivityRepository>();
        serviceCollection.AddScoped<IBalanceSheetReadRepository, BalanceSheetReadRepository>();
        serviceCollection.AddScoped<IIncomeStatementsReadRepository, IncomeStatementReadRepository>();
        serviceCollection.AddScoped<INonOperationIncomesRepository, NonOperationIncomesRepository>();
        serviceCollection.AddScoped<IStatusOfViableCompaniesRepository, StatusOfViableCompaniesRepository>();
        serviceCollection.AddScoped<IIndicesRepository, IndicesRepository>();
        serviceCollection.AddScoped<IFinancialStatementReadRepository, FinancialStatementReadRepository>();
        serviceCollection.AddScoped<IInterpretativeReportSummaryPage5Repository, InterpretativeReportSummaryPage5Repository>();
        serviceCollection.AddScoped<IAnnualAssemblyRepository, AnnualAssemblyRepository>();
        serviceCollection.AddScoped<IExtraAssemblyRepository, ExtraAssemblyRepository>();
        serviceCollection.AddScoped<IExtraAnnualAssemblyRepository, ExtraAnnualAssemblyRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddManufacturingEventDispatcher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<EventDataDispatcher>();
        return serviceCollection;
    }
}