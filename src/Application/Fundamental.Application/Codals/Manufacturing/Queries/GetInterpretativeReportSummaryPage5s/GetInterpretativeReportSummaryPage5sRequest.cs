using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

[HandlerCode(HandlerCode.GetInterpretativeReportSummaryPage5s)]
public sealed record GetInterpretativeReportSummaryPage5SRequest(
    string? Isin,
    int? FiscalYear,
    int? ReportMonth
) : PagingRequest, IRequest<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>>;