using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSectors;

[HandlerCode(HandlerCode.GetSectors)]
public sealed class GetSectorsRequest : IRequest<Response<List<GetSectorsResultDto>>>
{
    public string Filter { get; init; } = string.Empty;
}