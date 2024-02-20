using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Prices.Jobs.UpdateClosePrices;

[HandlerCode(HandlerCode.UpdateClosePrices)]
public sealed record UpdateClosePricesDataCommand(uint Days) : IRequest<Response>;