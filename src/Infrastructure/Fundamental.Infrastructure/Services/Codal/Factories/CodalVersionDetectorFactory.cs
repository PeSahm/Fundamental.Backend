using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Infrastructure.Services.Codal.Factories;

public class CodalVersionDetectorFactory(IServiceProvider serviceProvider) : ICodalVersionDetectorFactory
{
    public ICodalVersionDetector GetCodalVersionDetector(ReportingType reportingType, LetterType letterType, LetterPart letterPart)
    {
        return serviceProvider.GetRequiredKeyedService<ICodalVersionDetector>(reportingType, letterType, letterPart);
    }
}