using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

[HandlerCode(HandlerCode.GetInterpretativeReportSummaryPage5s)]
public sealed record GetInterpretativeReportSummaryPage5sRequest(
    string? Isin,
    int? FiscalYear,
    int? ReportMonth,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>>;
