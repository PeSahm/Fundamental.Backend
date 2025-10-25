using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateBalanceSheetData;

[HandlerCode(HandlerCode.UpdateBalanceSheetData)]
public record UpdateBalanceSheetDataRequest(int DaysBefore) : IRequest<Response>;