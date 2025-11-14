using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;

public sealed record AddBalanceSheetItem(BalanceSheetCategory CodalCategory, ushort CodalRow, decimal Value);