using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;

[HandlerCode(HandlerCode.GetIncomeStatements)]
public sealed record GetIncomeStatementsRequest(List<string> IsinList, ulong? TraceNo, ushort? FiscalYear, ushort? ReportMonth) :
    PagingRequest, IRequest<Response<Paginated<GetIncomeStatementsResultDto>>>;