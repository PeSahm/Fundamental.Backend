using Fundamental.Domain.Codals.Enums;

namespace Fundamental.Application.Statements.Commands.AddBalanceSheet;

public sealed record AddBalanceSheetItem(BalanceSheetCategory CodalCategory, ushort CodalRow, decimal Value);