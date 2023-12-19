using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codal.Commands.UpdateBalanceSheetData;

[HandlerCode(HandlerCode.UpdateBalanceSheetData)]
public record UpdateBalanceSheetDataRequest(int Days) : IRequest<Response>;