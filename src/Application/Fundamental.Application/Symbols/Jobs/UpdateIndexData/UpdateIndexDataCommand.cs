using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Jobs.UpdateIndexData;

[HandlerCode(HandlerCode.UpdateIndexData)]
public sealed record UpdateIndexDataCommand : IRequest;