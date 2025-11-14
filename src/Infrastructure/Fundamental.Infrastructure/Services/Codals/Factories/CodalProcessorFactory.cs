using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Infrastructure.Services.Codals.Factories;

public class CodalProcessorFactory(IServiceProvider serviceProvider) : ICodalProcessorFactory
{
    public ICodalProcessor GetCodalProcessor(
        string json,
        ReportingType reportingType,
        LetterType letterType,
        LetterPart letterPart
    )
    {
        ICodalVersionDetector versionDetector =
            serviceProvider.GetRequiredKeyedService<ICodalVersionDetector>(reportingType, letterType, letterPart);
        CodalVersion statementVersion = versionDetector.DetectVersion(json);

        return serviceProvider.GetRequiredKeyedService<ICodalProcessor>(reportingType, letterType, statementVersion, letterPart);
    }
}