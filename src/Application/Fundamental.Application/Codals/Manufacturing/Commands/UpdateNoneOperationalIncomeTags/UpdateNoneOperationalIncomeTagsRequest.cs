using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateNoneOperationalIncomeTags;

[HandlerCode(HandlerCode.UpdateNoneOperationalIncomeTags)]
public sealed record UpdateNoneOperationalIncomeTagsRequest(Guid Id, List<NoneOperationalIncomeTag> Tags) : IRequest<Response>;