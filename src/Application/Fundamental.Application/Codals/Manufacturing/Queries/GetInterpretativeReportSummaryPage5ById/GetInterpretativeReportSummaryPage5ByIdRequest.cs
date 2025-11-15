using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;

[HandlerCode(HandlerCode.GetInterpretativeReportSummaryPage5ById)]
public sealed record GetInterpretativeReportSummaryPage5ByIdRequest(
    Guid Id
) : IRequest<Response<GetInterpretativeReportSummaryPage5DetailItem>>;
