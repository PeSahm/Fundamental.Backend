using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.BuildingBlock.Models;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementList;

[HandlerCode(HandlerCode.GetFinancialStatements)]
public sealed record GetFinancialStatementListRequest(
    string[] IsinList,
    Guid? SectorCode,
    NumberRange<ulong>? MarketValueRange
) : PagingRequest, IRequest<Response<Paginated<GetFinancialStatementsResultDto>>>;