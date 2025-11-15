using Fundamental.Application.Codals.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ForceUpdateStatements;

[HandlerCode(HandlerCode.ForceUpdateStatements)]
public record ForceUpdateStatementRequest(ulong TraceNo, LetterPart LetterPart) : IRequest<Response>;