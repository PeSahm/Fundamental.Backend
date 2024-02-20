using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum CompanyType
{
    [Description("غیر از نهاد های مالی")]
    NoneFinancialInstitution = 0,

    [Description("سرمایه گذاری عام و هلدینگ عام")]
    PublicInvestmentAndHolding = 1,

    [Description("نهادهای مالی")]
    FinancialInstitutions = 2,

    [Description("نهادهای مالی تابعه")]
    SubsidiaryFinancialInstitutions = 3,

    [Description("نهادهای واسط")]
    IntermediaryInstitutions = 4,

    [Description("صندوق های سرمایه گذاری/ زمین و ساختمان")]
    InvestmentFunds = 5,

    [Description("شرکت های سبد گردان")]
    BasketCompanies = 6,

    [Description("شرکت های مشاور سرمایه گذاری")]
    InvestmentAdvisoryCompanies = 7,

    [Description("شرکت های پردازش اطلاعات مالی")]
    FinancialInformationProcessingCompanies = 8,

    [Description("شرکت های تامین سرمایه")]
    CapitalSupplyCompanies = 9,

    [Description("کانون ها")]
    Associations = 10,

    [Description("شرکت مدیریت دارایی مرکزی")]
    CentralAssetManagementCompany = 11,

    [Description("موسسات رتبه بندی")]
    RatingInstitutions = 12,

    [Description("اصل 44")]
    Article44 = 13,

    [Description("کارگزاران")]
    Brokers = 14,

    [Description("شرکت های دولتی")]
    GovernmentCompanies = 15,

    [Description("شرکت های معاف از ثبت")]
    ExemptCompanies = 16,

    UnKnown1 = 17
}