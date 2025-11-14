using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

[HandlerCode(HandlerCode.GetSymbols)]
public sealed class GetSymbolsRequest : IRequest<Response<List<GetSymbolsResultDto>>>
{
    public string Filter { get; init; } = string.Empty;

    public List<ReportingType> ReportingTypes { get; init; } = [];

    public bool ShowOfficialSymbolsOnly { get; init; } = true;
}