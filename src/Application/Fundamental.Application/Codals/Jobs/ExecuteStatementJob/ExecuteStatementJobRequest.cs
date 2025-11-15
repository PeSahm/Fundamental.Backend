using Fundamental.Application.Codals.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Jobs.ExecuteStatementJob;

[HandlerCode(HandlerCode.ExecuteStatementJobRequest)]
public record ExecuteStatementJobRequest(ulong TraceNo, LetterPart LetterPart, ReportingType? ReportingType) : IRequest<Response>;