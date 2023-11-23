using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

[HandlerCode(HandlerCode.GetSymbols)]
public sealed record GetSymbolsRequest(string Filter) : IRequest<Response<List<GetSymbolsResultDto>>>;