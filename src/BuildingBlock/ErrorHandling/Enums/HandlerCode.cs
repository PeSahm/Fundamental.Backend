namespace Fundamental.ErrorHandling.Enums;

public enum HandlerCode : ushort
{
    GetSymbols = 11_999,
    AddSymbolRelationship = 11_399,

    GetMonthlyActivities = 13_996,
    GetFinancialStatements = 13_997,
    AddMonthlyActivity = 13_398,
    AddFinancialStatement = 13_399,

    GetCustomerErrorMessages = 17_999,
}