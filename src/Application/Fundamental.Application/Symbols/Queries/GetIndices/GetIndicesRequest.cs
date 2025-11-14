using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetIndices;

[HandlerCode(HandlerCode.GetIndices)]
public sealed record GetIndicesRequest(string Isin, DateOnly? From, DateOnly? To) : IRequest<Response<GetIndicesResultDto>>;