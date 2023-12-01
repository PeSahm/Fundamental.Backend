using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetFinancialStatementById;

[HandlerCode(HandlerCode.GetFinancialStatementById)]
public sealed record GetFinancialStatementByIdRequest(Guid Id) : IRequest<Response<GetFinancialStatementsResultItem>>;