using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Services;

public static class ServiceExtensions
{
    /// <summary>
    /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
    /// <param name="reportingType">Codal Reporting Type. </param>
    /// <param name="letterType">Codal Letter Type. </param>
    /// <param name="letterPart">Different part of a report. </param>
    /// <returns>A service object of type <typeparamref name="T"/> or null if there is no such service.</returns>
    public static T GetRequiredKeyedService<T>(
        this IServiceProvider provider,
        ReportingType reportingType,
        LetterType letterType,
        LetterPart letterPart
    )
        where T : ICodalVersionDetector
    {
        ArgumentException.ThrowIfNullOrWhiteSpace("provider");

        if (provider is IKeyedServiceProvider keyedServiceProvider)
        {
            return (T)keyedServiceProvider.GetRequiredKeyedService(typeof(T), VersionDetectorKey(reportingType, letterType, letterPart));
        }

        throw new InvalidOperationException("Keyed Services Not Supported");
    }

    public static T GetRequiredKeyedService<T>(
        this IServiceProvider provider,
        ReportingType reportingType,
        LetterType letterType,
        CodalVersion version,
        LetterPart letterPart
    )
        where T : ICodalProcessor
    {
        ArgumentException.ThrowIfNullOrWhiteSpace("provider");

        if (provider is IKeyedServiceProvider keyedServiceProvider)
        {
            return (T)keyedServiceProvider.GetRequiredKeyedService(
                typeof(T),
                CodalProcessorKey(reportingType, letterType, version, letterPart));
        }

        throw new InvalidOperationException("Keyed Services Not Supported");
    }

    /// <summary>
    /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation"/> to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceCollection AddKeyedScopedCodalVersionDetector<TService, TImplementation>(
        this IServiceCollection services
    )
        where TService : ICodalVersionDetector
        where TImplementation : class, TService
    {
        return services.AddKeyedScoped(
            typeof(TService),
            VersionDetectorKey(TImplementation.ReportingType, TImplementation.LetterType, TImplementation.LetterPart),
            typeof(TImplementation));
    }

    /// <summary>
    /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation"/> to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceCollection AddKeyedScopedCodalProcessor<TService, TImplementation>(
        this IServiceCollection services
    )
        where TService : ICodalProcessor
        where TImplementation : class, TService
    {
        return services.AddKeyedScoped(
            typeof(TService),
            CodalProcessorKey(
                TImplementation.ReportingType,
                TImplementation.LetterType,
                TImplementation.CodalVersion,
                TImplementation.LetterPart),
            typeof(TImplementation));
    }

    private static string VersionDetectorKey(ReportingType reportingType, LetterType letterType, LetterPart letterPart)
    {
        return $"{reportingType}-{letterType}-{letterPart}";
    }

    private static string CodalProcessorKey(ReportingType reportingType, LetterType letterType, CodalVersion version, LetterPart letterPart)
    {
        return $"{reportingType}-{letterType}-{version}-{letterPart}";
    }
}