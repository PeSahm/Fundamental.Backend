using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

[HandlerCode(HandlerCode.GetFinancialStatementsData)]
public sealed record GetFinancialStatementsReqeust(string Isin)
    : IRequest<Response<GetFinancialStatementsResultDto>>;