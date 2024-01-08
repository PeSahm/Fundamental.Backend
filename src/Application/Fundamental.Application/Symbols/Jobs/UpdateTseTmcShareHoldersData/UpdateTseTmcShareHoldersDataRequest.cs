using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Jobs.UpdateTseTmcShareHoldersData;

[HandlerCode(HandlerCode.UpdateTseTmcShareHoldersData)]
public sealed record UpdateTseTmcShareHoldersDataRequest : IRequest<Response>;