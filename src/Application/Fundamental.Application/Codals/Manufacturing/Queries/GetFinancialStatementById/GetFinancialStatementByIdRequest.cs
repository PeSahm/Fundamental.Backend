using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementById;

[HandlerCode(HandlerCode.GetFinancialStatementById)]
public sealed record GetFinancialStatementByIdRequest(Guid Id) : IRequest<Response<GetFinancialStatementsResultItem>>;