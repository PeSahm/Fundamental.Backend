using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;

[HandlerCode(HandlerCode.GetNonOperationIncomes)]
public sealed record GetNonOperationIncomesRequest(
    string[] IsinList,
    NoneOperationalIncomeTagStatus? TagStatus,
    ushort? Year,
    ushort? ReportMonth
) : PagingRequest, IRequest<Response<Paginated<GetNonOperationIncomesResultItem>>>;