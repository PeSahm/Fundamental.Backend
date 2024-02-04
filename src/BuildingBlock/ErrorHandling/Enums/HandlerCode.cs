namespace Fundamental.ErrorHandling.Enums;

public enum HandlerCode : ushort
{
    UpdateTseTmcShareHoldersData = 11_199,
    GetSymbols = 11_999,
    ApproveSymbolShareHolder = 11_394,
    RejectSymbolShareHolder = 11_395,
    GetSymbolShareHolders = 11_396,
    GetSymbolRelationById = 11_397,
    GetSymbolRelations = 11_398,
    AddSymbolRelationship = 11_399,

    UpdateCodalPublisher = 13_195,
    UpdateNonOperationIncomeAndExpenses = 13_196,
    UpdateIncomeStatementData = 13_197,
    UpdateBalanceSheetData = 13_198,
    UpdateMonthlyActivityData = 13_199,

    UpdateClosePrices = 14_100,

    GetIncomeStatementDetails = 13_384,
    GetBalanceSheetDetails = 13_385,
    AddIncomeStatement = 13_386,
    GetIncomeStatements = 13_387,
    GetIncomeStatementSortList = 13_388,
    GetBalanceSheetSortList = 13_389,
    GetBalanceSheet = 13_390,
    AddBalanceSheet = 13_391,
    UpdateMonthlyActivity = 13_392,
    UpdateFinancialStatement = 13_393,
    GetMonthlyActivityById = 13_394,
    GetFinancialStatementById = 13_395,
    GetMonthlyActivities = 13_396,
    GetFinancialStatements = 13_397,
    AddMonthlyActivity = 13_398,
    AddFinancialStatement = 13_399,

    GetCustomerErrorMessages = 17_999,
}