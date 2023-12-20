using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetMonthlyActivities;

[HandlerCode(HandlerCode.GetMonthlyActivities)]
public record GetMonthlyActivitiesRequest(
    string[] IsinList,
    ushort? Year,
    ushort? ReportMonth
) : PagingRequest, IRequest<Response<Paginated<GetMonthlyActivitiesResultItem>>>;