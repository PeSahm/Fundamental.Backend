using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Application.Codal.Services;

public interface ICodalProcessorFactory
{
    ICodalProcessor GetCodalProcessor(string json, ReportingType reportingType, LetterType letterType);
}