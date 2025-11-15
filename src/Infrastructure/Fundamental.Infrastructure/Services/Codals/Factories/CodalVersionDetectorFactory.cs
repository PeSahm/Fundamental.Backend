using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Infrastructure.Services.Codals.Factories;

public class CodalVersionDetectorFactory(IServiceProvider serviceProvider) : ICodalVersionDetectorFactory
{
    public ICodalVersionDetector GetCodalVersionDetector(ReportingType reportingType, LetterType letterType, LetterPart letterPart)
    {
        return serviceProvider.GetRequiredKeyedService<ICodalVersionDetector>(reportingType, letterType, letterPart);
    }
}