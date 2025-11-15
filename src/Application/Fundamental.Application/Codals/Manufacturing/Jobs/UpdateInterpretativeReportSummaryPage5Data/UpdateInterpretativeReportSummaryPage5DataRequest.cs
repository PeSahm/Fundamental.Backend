using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateInterpretativeReportSummaryPage5Data;

[HandlerCode(HandlerCode.UpdateInterpretativeReportSummaryPage5Data)]
public sealed record UpdateInterpretativeReportSummaryPage5DataRequest(int Days) : IRequest<Response>;
