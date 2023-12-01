namespace Fundamental.ErrorHandling.Enums;

public enum HandlerCode : ushort
{
    GetSymbols = 11_999,
    AddSymbolRelationship = 11_399,

    GetMonthlyActivityById = 13_394,
    GetFinancialStatementById = 13_395,
    GetMonthlyActivities = 13_396,
    GetFinancialStatements = 13_397,
    AddMonthlyActivity = 13_398,
    AddFinancialStatement = 13_399,

    GetCustomerErrorMessages = 17_999,
}