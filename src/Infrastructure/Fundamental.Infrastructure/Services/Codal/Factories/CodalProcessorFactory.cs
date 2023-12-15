using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Infrastructure.Services.Codal.Factories;

public class CodalProcessorFactory(IServiceProvider serviceProvider) : ICodalProcessorFactory
{
    public ICodalProcessor GetCodalProcessor(
        string json,
        ReportingType reportingType,
        LetterType letterType
    )
    {
        ICodalVersionDetector versionDetector = serviceProvider.GetRequiredKeyedService<ICodalVersionDetector>(reportingType, letterType);
        CodalVersion statementVersion = versionDetector.DetectVersion(json);

        return serviceProvider.GetRequiredKeyedService<ICodalProcessor>(reportingType, letterType, statementVersion);
    }
}