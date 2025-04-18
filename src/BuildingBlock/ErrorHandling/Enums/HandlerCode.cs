namespace Fundamental.ErrorHandling.Enums;

public enum HandlerCode : ushort
{
    UpdateIndexData = 11_198,
    UpdateTseTmcShareHoldersData = 11_199,
    GetIndices = 11_998,
    GetSymbols = 11_999,
    ApproveSymbolShareHolder = 11_394,
    RejectSymbolShareHolder = 11_395,
    GetSymbolShareHolders = 11_396,
    GetSymbolRelationById = 11_397,
    GetSymbolRelations = 11_398,
    AddSymbolRelationship = 11_399,

    UpdateFinancialStatementsData = 13_191,
    UpdateTheStatusOfViableCompany = 13_192,
    ExecuteStatementJobRequest = 13_193,
    UpdateSymbolData = 13_194,
    UpdateCodalPublisher = 13_195,
    UpdateNonOperationIncomeAndExpenses = 13_196,
    UpdateIncomeStatementData = 13_197,
    UpdateBalanceSheetData = 13_198,
    UpdateMonthlyActivityData = 13_199,

    UpdateClosePrices = 14_100,

    UpdateFinancialStatementSales = 13_377,
    GetFinancialStatementsData = 13_378,
    UpdateNoneOperationalIncomeTags = 13_379,
    RejectStatusOfViableCompany = 13_380,
    ApproveStatusOfViableCompany = 13_381,
    GetGetStatusOfViableCompanies = 13_382,
    ForceUpdateStatements = 13_383,
    GetIncomeStatementDetails = 13_384,
    GetBalanceSheetDetails = 13_385,
    AddIncomeStatement = 13_386,
    GetIncomeStatements = 13_387,
    GetIncomeStatementSortList = 13_388,
    GetBalanceSheetSortList = 13_389,
    GetBalanceSheet = 13_390,
    AddBalanceSheet = 13_391,
    UpdateMonthlyActivity = 13_392,
    UpdateFinancialStatementClosePrice = 13_393,
    GetMonthlyActivityById = 13_394,
    GetFinancialStatementById = 13_395,
    GetMonthlyActivities = 13_396,
    GetFinancialStatements = 13_397,
    AddMonthlyActivity = 13_398,
    GetNonOperationIncomes = 13_399,

    GetCustomerErrorMessages = 17_999,

    SaveFair = 19_999
}