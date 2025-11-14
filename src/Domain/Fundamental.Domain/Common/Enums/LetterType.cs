using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#pragma warning disable S2479 // Control characters in string literals should be replaced with escape sequences

namespace Fundamental.Domain.Common.Enums;

/// <summary>
///     انواع اطلاعیه.
/// </summary>
public enum LetterType
{
    [Description("نامه ")]
    [Display(Name = "")]
    Letter = 0,

    [Description("اولین پیش بینی درآمد هر سهم")]
    [Display(Name = "")]
    InitialEarningsPerShareForecast = 1,

    [Description("پیش بینی درآمد هر سهم")]
    [Display(Name = "")]
    EarningsPerShareForecast = 2,

    [Description("اطلاعات و صورتهای مالی میاندوره ای")]
    [Display(Name = "ن-10")]
    InterimStatement = 6,

    [Description("صورت وضعیت پورتفوی")]
    [Display(Name = "ن-31")]
    PortfolioStatement = 8,

    [Description("افشاي اطلاعات با اهميت")]
    [Display(Name = "ن-20")]
    MaterialInformationDisclosure = 11,

    [Description("سایر")]
    [Display(Name = "")]
    Other = 12,

    [Description("آگهي دعوت به مجمع عمومي عادي ساليانه")]
    [Display(Name = "ن-50")]
    AnnualGeneralMeetingInvitation = 16,

    [Description("آگهي دعوت به مجمع عمومي عادي بطور فوق العاده")]
    [Display(Name = "ن-54")]
    OrdinaryGeneralMeetingExtraordinaryInvitation = 17,

    [Description("آگهي دعوت به مجمع عمومي فوق العاده")]
    [Display(Name = "ن-56")]
    ExtraordinaryGeneralMeetingInvitation = 18,

    [Description("معرفي يا تغيير در ترکيب اعضاي هيئت‌مديره")]
    [Display(Name = "ن-45")]
    BoardCompositionChange = 19,

    [Description("تصميمات مجمع عمومي عادي ساليانه")]
    [Display(Name = "ن-52")]
    AnnualGeneralMeetingDecisions = 20,

    [Description("تصميمات مجمع عمومي عادي به‌طور فوق‌العاده")]
    [Display(Name = "ن-55")]
    OrdinaryGeneralMeetingExtraordinaryDecisions = 21,

    [Description("تصميمات مجمع عمومي فوق‌العاده")]
    [Display(Name = "ن-57")]
    ExtraordinaryGeneralMeetingDecisions = 22,

    [Description("زمان تشکيل جلسه هيئت‌مديره در خصوص افزايش سرمايه")]
    [Display(Name = "ن-71")]
    BoardMeetingScheduleCapitalIncrease = 23,

    [Description("تصميمات هيئت‌مديره در خصوص افزايش سرمايه")]
    [Display(Name = "ن-73")]
    BoardDecisionsCapitalIncrease = 24,

    [Description("مهلت استفاده از حق تقدم خريد سهام")]
    [Display(Name = "ن-64")]
    RightsIssueSubscriptionPeriod = 25,

    [Description("لغو  آگهي (اطلاعيه) دعوت به مجمع عمومي")]
    [Display(Name = "ن-58")]
    MeetingInvitationCancellation = 26,

    [Description("اعلاميه پذيره نويسي عمومي")]
    [Display(Name = "ن-65")]
    PublicOfferingAnnouncement = 27,

    [Description("آگهي ثبت افزايش سرمايه")]
    [Display(Name = "ن-67")]
    CapitalIncreaseRegistrationNotice = 28,

    [Description("زمانبندي پرداخت سود")]
    [Display(Name = "ن-13")]
    DividendPaymentSchedule = 30,

    [Description("گزارش فعاليت هيئت مديره")]
    [Display(Name = "ن-11")]
    BoardOfDirectorsReport = 32,

    [Description("رتبه بندي شرکتهاي از نظر کيفيت افشا و اطلاع رساني")]
    [Display(Name = "س-2")]
    DisclosureQualityRanking = 34,

    [Description("وضعيت شرکتها از نظر تعداد روزهاي گشايش و معاملاتي")]
    [Display(Name = "س-3")]
    TradingDaysStatus = 36,

    [Description("نامه ها و گزارش هاي نهادهاي مالي")]
    [Display(Name = "")]
    FinancialInstitutionLetters = 38,

    [Description("مدارک و مستندات درخواست افزایش سرمایه")]
    [Display(Name = "ن-62")]
    CapitalIncreaseRequestDocuments = 45,

    [Description("ثبت اوراق مشارکت")]
    [Display(Name = "ن-84")]
    ParticipationBondRegistration = 46,

    [Description("ثبت اوراق مرابحه")]
    [Display(Name = "ن-83")]
    MurabahaBondRegistration = 47,

    [Description("ثبت اوراق اجاره")]
    [Display(Name = "ن-80")]
    IjaraBondRegistration = 48,

    [Description("آگهي دعوت به مجمع عمومي عادي ساليانه نوبت دوم")]
    [Display(Name = "ن-53")]
    SecondAnnualGeneralMeetingInvitation = 52,

    [Description("اطلاعیه تکمیلی صدور مجوز افزایش سرمایه")]
    [Display(Name = "ن-62")]
    CapitalIncreaseLicenseSupplement = 53,

    [Description("لغو اطلاعيه زمان تشکيل جلسه هيات مديره در خصوص افزايش سرمايه")]
    [Display(Name = "ن-72")]
    CapitalIncreaseBoardMeetingCancellation = 54,

    [Description("پیشنهاد هیئت مدیره به مجمع عمومی فوق العاده در خصوص افزایش سرمایه")]
    [Display(Name = "ن-60")]
    BoardProposalCapitalIncrease = 55,

    [Description("اظهارنظر حسابرس و بازرس قانونی نسبت به گزارش توجیهی هیئت مدیره در خصوص افزایش سرمایه")]
    [Display(Name = "ن-61")]
    AuditorOpinionCapitalIncreaseReport = 56,

    [Description("تصمیم هیئت مدیره به انجام افزایش سرمایه تفویض شده در مجمع فوق العاده")]
    [Display(Name = "ن-70")]
    BoardResolutionCapitalIncreaseDelegated = 57,

    [Description("گزارش فعاليت ماهانه")]
    [Display(Name = "ن-30")]
    MonthlyActivity = 58,

    [Description("مشمول اصل 44 - اساسنامه شرکت ")]
    [Display(Name = "")]
    Article44CompanyCharter = 59,

    [Description("مشخصات کميته حسابرسي و واحد حسابرسي داخلي")]
    [Display(Name = "ن-41")]
    AuditCommitteeDetails = 60,

    [Description("مشمول اصل 44 - تغییرات اساسنامه شرکت")]
    [Display(Name = "")]
    Article44CharterChanges = 61,

    [Description("مشمول اصل 44 - گزارش توجیهی افزایش سرمایه به همراه گزارش حسابرس و بازرس قانونی")]
    [Display(Name = "")]
    Article44CapitalIncreaseJustification = 62,

    [Description("مشمول اصل 44 - آگهی ثبت افزایش سرمایه")]
    [Display(Name = "")]
    Article44CapitalIncreaseRegistrationNotice = 63,

    [Description("مشمول اصل 44 - دعوت به مجمع عمومی عادی سالیانه")]
    [Display(Name = "")]
    Article44AGMInvitation = 64,

    [Description("مشمول اصل 44 - دعوت به مجمع عمومی عادی سالیانه نوبت دوم")]
    [Display(Name = "")]
    Article44SecondAGMInvitation = 65,

    [Description("مشمول اصل 44 - دعوت به مجمع عادی به طور فوق العاده نوبت دوم")]
    [Display(Name = "")]
    Article44SecondOrdinaryExtraordinaryInvitation = 66,

    [Description("مشمول اصل 44 - دعوت به مجمع عادی به طور فوق العاده")]
    [Display(Name = "")]
    Article44OrdinaryExtraordinaryInvitation = 67,

    [Description("مشمول اصل 44 - دعوت به مجمع عمومی فوق العاده")]
    [Display(Name = "")]
    Article44EGMInvitation = 68,

    [Description("مشمول اصل 44 - تصمیمات مجمع عادی به طور فوق العاده")]
    [Display(Name = "")]
    Article44OrdinaryExtraordinaryDecisions = 69,

    [Description("مشمول اصل 44 - تصمیمات مجمع عمومی عادی سالیانه")]
    [Display(Name = "")]
    Article44AGMDecisions = 70,

    [Description("مشمول اصل 44 - تصمیمات مجمع عمومی عادی سالیانه نوبت دوم ")]
    [Display(Name = "")]
    Article44SecondAGMDecisions = 71,

    [Description("مشمول اصل 44 - تصمیمات مجمع عادی به طور فوق العاده نوبت دوم ")]
    [Display(Name = "")]
    Article44SecondOrdinaryExtraordinaryDecisions = 72,

    [Description("مشمول اصل 44 - تصمیمات مجمع عمومی فوق العاده")]
    [Display(Name = "")]
    Article44EGMDecisions = 73,

    [Description("مشمول اصل 44 - گزارش فعالیت هیات مدیره به مجمع")]
    [Display(Name = "")]
    Article44BoardReport = 74,

    [Description("مشمول اصل 44 - معرفی با تغییر ترکیب هیات مدیره ")]
    [Display(Name = "")]
    Article44BoardCompositionChange = 75,

    [Description("مشمول اصل 44 - اطلاعات و صورتهای مالی پایان دوره به همراه گزارش حسابرس و بازرس قانونی")]
    [Display(Name = "")]
    Article44YearEndFinancialStatements = 76,

    [Description("گزارش هیات مدیره به مجمع")]
    [Display(Name = "ن-11")]
    BoardReportToAGM = 79,

    [Description("گزارش توجیهی افزایش سرمایه")]
    [Display(Name = "ن-60")]
    CapitalIncreaseJustificationReport = 80,

    [Description("آگهی ثبت مصوبات مجمع و هیات مدیره در روزنامه رسمی")]
    [Display(Name = "ن-42")]
    OfficialGazetteResolutionsRegistration = 81,

    [Description("تمدید مهلت استفاده از مجوز افزایش سرمایه")]
    [Display(Name = "ن-63")]
    CapitalIncreaseLicenseExtension = 89,

    [Description("گزارش کنترل هاي داخلي")]
    [Display(Name = "ن-12")]
    InternalControlsReport = 90,

    [Description("مشمول اصل 44 - مشخصات شرکت و مديران ")]
    [Display(Name = "")]
    Article44CompanyAndDirectorsInfo = 91,

    [Description("ثبت اوراق رهني")]
    [Display(Name = "ن-81")]
    MortgageBondRegistration = 93,

    [Description("ثبت اوراق سفارش ساخت")]
    [Display(Name = "ن-82")]
    ConstructionOrderBondRegistration = 94,

    [Description("مشمول اصل 44 - صورت وضعيت پورتفو سرمايه گذاري ها در سهام و سهم الشرکه")]
    [Display(Name = "")]
    Article44PortfolioStatement = 95,

    [Description("مشمول اصل 44 - پیش بینی بودجه سالانه")]
    [Display(Name = "")]
    Article44AnnualBudgetForecast = 96,

    [Description("مشمول اصل 44 - تصمیمات مجمع عمومی فوق العاده نوبت دوم")]
    [Display(Name = "")]
    Article44SecondEGMDecisions = 97,

    [Description("مشمول اصل 44 - اطلاعات صورت مالي تلفيقي پایان دوره به همراه گزارش حسابرسی و بازرس قانونی")]
    [Display(Name = "")]
    Article44ConsolidatedFinancialStatements = 98,

    [Description("مشمول اصل 44 - دعوت به مجمع عمومی فوق العاده نوبت دوم")]
    [Display(Name = "")]
    Article44SecondEGMInvitation = 99,

    [Description("مشمول اصل 44 - اطلاعات صورت مالی میان دوره ای")]
    [Display(Name = "")]
    Article44InterimFinancialInformation = 100,

    [Description("مشمول اصل 44 - درخواست ثبت صورتجلسه نزد مرجع ثبت شرکتها")]
    [Display(Name = "")]
    Article44MinutesRegistrationRequest = 101,

    [Description("مشمول اصل 44 - سایر موارد")]
    [Display(Name = "")]
    Article44Other = 105,

    [Description("نتایج حاصل از فروش حق تقدم های استفاده نشده")]
    [Display(Name = "ن-66")]
    UnsoldRightsProceeds = 119,

    [Description("اساسنامه شرکت مصوب مجمع عمومی فوق العاده")]
    [Display(Name = "ن-43")]
    PostEGMApprovedCharter = 120,

    [Description("آگهي دعوت به مجمع صندوق سرمايه گذاري")]
    [Display(Name = "")]
    InvestmentFundMeetingInvitation = 121,

    [Description("تصميمات مجمع صندوق سرمايه گذاري")]
    [Display(Name = "")]
    InvestmentFundMeetingDecisions = 122,

    [Description("گزارش خالص ارزش هر واحد سرمایه گذاری به بهای تمام شده")]
    [Display(Name = "")]
    FundNAVAtCost = 123,

    [Description("گزارش خالص ارزش هر واحد سرمایه گذاری به ارزش روز")]
    [Display(Name = "")]
    FundNAVAtMarket = 124,

    [Description("گزارش عملکرد مدیر صندوق")]
    [Display(Name = "")]
    FundManagerPerformanceReport = 125,

    [Description("شفاف سازی در خصوص شایعه، خبر یا گزارش منتشر شده")]
    [Display(Name = "ن-21")]
    RumorClarification = 128,

    [Description("عدم پاسخگویی ناشر در خصوص شایعه، خبر یا گزارش منتشر شده")]
    [Display(Name = "س-4")]
    NonResponsivePublisherRumor = 129,

    [Description("عدم پاسخگویی ناشر در خصوص علت تخطی از الزامات دستورالعمل پذیرش اوراق بهادار")]
    [Display(Name = "ب-1، ف ب-1")]
    NonResponsivePublisherListingRequirements = 130,

    [Description("تعليق ورقه بهادار ناشر به علت عدم رعايت الزامات دستورالعمل پذيرش اوراق بهادار")]
    [Display(Name = "ب-2، ف ب-2")]
    SecuritySuspensionListingRequirements = 131,

    [Description("اعطاي فرصت به ناشر جهت رعايت دستورالعمل پذيرش اوراق بهادار")]
    [Display(Name = "ب-3، ف ب-3")]
    GracePeriodListingRequirements = 132,

    [Description("تمديد فرصت اعطا شده به ناشر جهت رعايت دستورالعمل پذيرش اوراق بهادار")]
    [Display(Name = "ب-4، ف ب-4")]
    GracePeriodExtensionListingRequirements = 133,

    [Description("تعليق ورقه بهادار ناشر بعد از فرصت اعطا شده")]
    [Display(Name = "ب-5، ف ب-5")]
    SecuritySuspensionAfterGracePeriod = 134,

    [Description("لغو پذيرش اوراق بهادار")]
    [Display(Name = "ب-10، ف ب-10")]
    Delisting = 135,

    [Description("توقف نماد معاملاتي ناشر به استناد ماده 16 مکرر / 12 مکرر دستورالعمل اجرايي نحوه انجام معاملات ")]
    [Display(Name = "س-5")]
    SymbolHaltArticle16 = 136,

    [Description("بازگشايي نماد معاملاتي ناشر به استناد ماده 16 مکرر/ 12 مکرر دستورالعمل اجرايي نحوه انجام معاملات ")]
    [Display(Name = "س-6")]
    SymbolReopeningArticle16 = 137,

    [Description("عدم پاسخگويي ناشر در خصوص علت نوسان قيمت سهام")]
    [Display(Name = "ب-7، ف ب-7")]
    NonResponsivePublisherPriceFluctuation = 138,

    [Description("عدم پاسخگويي ناشر در خصوص لزوم برگزاري کنفرانس اطلاع رساني و افشاي اهم مطالب مطرح شده")]
    [Display(Name = "ب-8، ف ب-8")]
    NonResponsivePublisherConferenceDisclosure = 139,

    [Description("توقف نماد معاملاتي ناشر به استناد ماده  19 مکرر/ 12 مکرر 3 دستورالعمل اجرايي نحوه انجام معاملات ")]
    [Display(Name = "س-7")]
    SymbolHaltArticle19 = 140,

    [Description("تعليق نماد معاملاتي ناشر به استناد ماده  19 مکرر 1/  12 مکرر 4 دستورالعمل اجرايي نحوه انجام معاملات ")]
    [Display(Name = "س-8")]
    SymbolSuspensionArticle19 = 141,

    [Description("عدم پاسخگويي ناشر در خصوص لزوم رعايت ماده 19 مکرر 1/ 12 مکرر 4دستورالعمل اجرايي نحوه انجام معاملات")]
    [Display(Name = "س-9")]
    NonResponsivePublisherArticle19 = 142,

    [Description("ادامه تعليق نماد معاملاتي پيرو درخواست ناشر/ به منظور بررسي بيشتر اطلاعات ناشر")]
    [Display(Name = "س-12")]
    ContinuedSuspensionAtPublisherRequest = 143,

    [Description("تداوم تعليق نماد معاملاتي ناشر به استناد ماده 19 مکرر 1/ 12 مکرر 4 دستورالعمل اجرايي نحوه انجام معاملات")]
    [Display(Name = "س-13")]
    ContinuedSuspensionArticle19 = 144,

    [Description("بازگشايي نماد معاملاتي ناشر پس از پايان مهلت مقرر تعليق نماد")]
    [Display(Name = "س-14")]
    SymbolReopeningAfterSuspensionPeriod = 145,

    [Description("لغو پذيرش اوراق بهادار تعليق شده")]
    [Display(Name = "ب-6، ف ب-6")]
    DelistingSuspendedSecurity = 146,

    [Description("شفاف سازي در خصوص نوسان قيمت سهام")]
    [Display(Name = "ن-22")]
    PriceVolatilityClarification = 147,

    [Description("اطلاعات حاصل از برگزاري کنفرانس اطلاع رساني")]
    [Display(Name = "ن-23")]
    ConferenceInformationDisclosure = 148,

    [Description("درخواست ارايه مهلت جهت رعايت ماده  19 مکرر 1/ 12 مکرر 4 دستورالعمل اجرايي نحوه انجام معاملات")]
    [Display(Name = "ن-24")]
    GracePeriodRequestArticle19 = 149,

    [Description("ضرورت برگزاري کنفرانس اطلاع رساني")]
    [Display(Name = "ب-9، ف ب-9")]
    ConferenceRequirement = 150,

    [Description("صورت وضعيت پرتفوي صندوق")]
    [Display(Name = "")]
    FundPortfolioStatement = 151,

    [Description("امیدنامه پذیرش در بورس اوراق بهادار تهران /فرابورس ایران")]
    [Display(Name = "")]
    IPOProspectus = 152,

    [Description("خروج شرکت ازشرایط معامله تحت احتیاط")]
    [Display(Name = "س-15")]
    CautionaryTradingExit = 153,

    [Description("لغو آگهي دعوت به مجمع صندوق سرمايه گذاري")]
    [Display(Name = "")]
    FundMeetingInvitationCancellation = 154,

    [Description("سهام شناور آزاد شرکت هاي پذيرفته شده در بورس و فرابورس")]
    [Display(Name = "س-1")]
    FreeFloatSharesReport = 155,

    [Description("برنامه ناشر جهت خروج از شمولیت ماده 141 لایحه قانونی اصلاحی قسمتی از قانون تجارت")]
    [Display(Name = "ن-25")]
    Capital141ExitPlan = 156,

    [Description("اعطای مهلت توسط هیئت پذیرش اوراق بهادار جهت خروج ناشر از شمولیت ماده 141 لایحه قانونی اصلاحی قسمتی از قانون تجارت")]
    [Display(Name = "ب-11، ف ب-11")]
    GracePeriodByListingCommittee141 = 157,

    [Description("تعلیق نماد معاملاتی ناشر پس از پایان اعطا شده جهت معامله در شرایط معامله تحت احتیاط")]
    [Display(Name = "س-16")]
    SuspensionAfterCautionPeriod = 158,

    [Description("آگهی ثبت تصمیمات مجمع عادی سالیانه")]
    [Display(Name = "ن-42")]
    AGMDecisionsGazetteNotice = 159,

    [Description("آگهی ثبت تصمیمات مجمع عمومی عادی بطور فوق العاده")]
    [Display(Name = "ن-42")]
    OrdinaryExtraordinaryDecisionsGazetteNotice = 160,

    [Description("آگهی ثبت تصمیمات مجمع عمومی فوق العاده (بجز تغییرات سرمایه)")]
    [Display(Name = "ن-42")]
    EGMDecisionsGazetteNotice = 161,

    [Description("آگهی ثبت تصمیمات مجمع عمومی مؤسس")]
    [Display(Name = "ن-42")]
    FoundersMeetingDecisionsGazetteNotice = 162,

    [Description("تغییر نشانی")]
    [Display(Name = "ن-80")]
    AddressChange = 163,

    [Description("توزیع گواهی نامه نقل و انتقال و سپرده سهام")]
    [Display(Name = "ن-69")]
    ShareCertificateDistribution = 164,

    [Description("درخواست تکمیل مشخصات سهامداران")]
    [Display(Name = "ن-81")]
    ShareholderInfoCompletionRequest = 165,

    [Description("مجوز بانک مرکزی جهت برگزاری مجمع عمومی عادی سالیانه")]
    [Display(Name = "ن-59")]
    CentralBankAGMPermission = 166,

    [Description("آگهی مطالبه مبلغ پرداخت نشده سرمایه")]
    [Display(Name = "ن-68")]
    UnpaidCapitalCallNotice = 167,

    [Description(
        "تعلیق نماد معاملاتی ناشر به استناد بند 6 ماده 13مکرر 2 دستورالعمل اجرایی نحوه انجام معاملات اوراق بهادار در فرابورس ایران")]
    [Display(Name = "ف ب-12")]
    SymbolSuspensionFraboorsArticle13_6 = 168,

    [Description(
        "تعلیق نماد معاملاتی ناشر به استناد بند 7 ماده 13مکرر2 دستورالعمل اجرایی نحوه انجام معاملات اوراق بهادار در فرابورس ایران\t")]
    [Display(Name = "ف ب-13")]
    SymbolSuspensionFraboorsArticle13_7 = 169,

    [Description(
        "ادامه تعلیق نماد معاملاتی ناشر به استناد بند 7 ماده 13مکرر 2 دستورالعمل اجرایی نحوه انجام معاملات اوراق بهادار در فرابورس ایران")]
    [Display(Name = "ف ب-13")]
    ContinuedSuspensionFraboorsArticle13_7 = 170,

    [Description("بازگشایی نماد معاملاتی ناشر پس از اتمام دوره تعلیق و عدم رفع ")]
    [Display(Name = "ف ب-14")]
    SymbolReopeningFraboorsAfterSuspension = 171,

    [Description("عدم پاسخگویی ناشر در خصوص شایعه، خبر یا گزارش منتشر شده - فرابورس")]
    [Display(Name = "ف ب-15")]
    NonResponsivePublisherRumorFraboors = 172,

    [Description("توضیحات در خصوص اطلاعات و صورت های مالی منتشر شده")]
    [Display(Name = "ن-26")]
    FinancialStatementsClarification = 174,

    [Description("درخواست اطلاعات سهامداران و تعهدنامه حفظ محرمانگی")]
    [Display(Name = "")]
    ShareholderInfoRequestConfidentiality = 175,

    [Description("شمول ماده 141 لایحه قانونی اصلاح قسمتی از قانون تجارت برای سومین سال متوالی/ چهارمین سال متناوب طی 5 سال گذشته")]
    [Display(Name = "ب-17، ف ب-17")]
    Article141ThirdConsecutiveYear = 178,

    [Description("اظهارنامه مالیاتی")]
    [Display(Name = "")]
    TaxDeclaration = 180,

    [Description("عدم فعالیت شرکت با اعلام به حوزه مالیاتی")]
    [Display(Name = "")]
    CompanyInactivityTaxNotice = 181,

    [Description("آگهی دعوت از داوطلبین جهت عضویت در هیات مدیره")]
    [Display(Name = "N-82")]
    BoardNomineeInvitationNotice = 188,

    [Description("گزارش ارزشیابی سهام")]
    [Display(Name = "N-83")]
    ShareValuationReport = 189,

    [Description("افشای جزییات زمین و ساختمان")]
    [Display(Name = "N-84")]
    LandBuildingDetailsDisclosure = 190,

    [Description("شرکت هاي دولتي - اساسنامه شرکت")]
    [Display(Name = "")]
    StateCompanyCharter = 191,

    [Description("شرکت هاي دولتي - تغييرات اساسنامه شرکت")]
    [Display(Name = "")]
    StateCompanyCharterChanges = 192,

    [Description("شرکت هاي دولتي - گزارش توجيهي افزايش سرمايه به همراه گزارش حسابرس و بازرس قانوني")]
    [Display(Name = "")]
    StateCompanyCapitalIncreaseJustification = 193,

    [Description("شرکت هاي دولتي - آگهي ثبت افزايش سرمايه")]
    [Display(Name = "")]
    StateCompanyCapitalIncreaseRegistrationNotice = 194,

    [Description("شرکت هاي دولتي - دعوت به مجمع عمومي عادي ساليانه")]
    [Display(Name = "")]
    StateCompanyAGMInvitation = 195,

    [Description("شرکت هاي دولتي - دعوت به مجمع عمومي عادي ساليانه نوبت دوم")]
    [Display(Name = "")]
    StateCompanySecondAGMInvitation = 196,

    [Description("شرکت هاي دولتي - دعوت به مجمع عادي به طور فوق العاده نوبت دوم")]
    [Display(Name = "")]
    StateCompanySecondOrdinaryExtraordinaryInvitation = 197,

    [Description("شرکت هاي دولتي - دعوت به مجمع عمومي عادي به طور فوق العاده")]
    [Display(Name = "")]
    StateCompanyOrdinaryExtraordinaryInvitation = 198,

    [Description("شرکت هاي دولتي - دعوت به مجمع عمومي فوق العاده")]
    [Display(Name = "")]
    StateCompanyEGMInvitation = 199,

    [Description("شرکت هاي دولتي - تصميمات مجمع عادي به طور فوق العاده")]
    [Display(Name = "")]
    StateCompanyOrdinaryExtraordinaryDecisions = 200,

    [Description("شرکت هاي دولتي - تصميمات مجمع عمومي عادي ساليانه")]
    [Display(Name = "")]
    StateCompanyAGMDecisions = 201,

    [Description("شرکت هاي دولتي - تصميمات مجمع عمومي عادي ساليانه نوبت دوم ")]
    [Display(Name = "")]
    StateCompanySecondAGMDecisions = 202,

    [Description("شرکت هاي دولتي - تصميمات مجمع عادي به طور فوق العاده نوبت دوم")]
    [Display(Name = "")]
    StateCompanySecondOrdinaryExtraordinaryDecisions = 203,

    [Description("شرکت هاي دولتي - تصميمات مجمع عمومي فوق العاده")]
    [Display(Name = "")]
    StateCompanyEGMDecisions = 204,

    [Description("شرکت هاي دولتي - گزارش فعاليت هيات مديره به مجمع")]
    [Display(Name = "")]
    StateCompanyBoardReport = 205,

    [Description("شرکت هاي دولتي - معرفي با تغيير ترکيب هيات مديره")]
    [Display(Name = "")]
    StateCompanyBoardChanges = 206,

    [Description("شرکت هاي دولتي - اطلاعات و صورتهاي مالي پايان دوره به همراه گزارش حسابرس و بازرس قانوني")]
    [Display(Name = "")]
    StateCompanyYearEndFinancialStatements = 207,

    [Description("شرکت هاي دولتي - صورت وضعيت پورتفو سرمايه گذاري ها در سهام و سهم الشرکه")]
    [Display(Name = "")]
    StateCompanyPortfolioStatement = 208,

    [Description("شرکت هاي دولتي - پيش بيني بودجه سالانه")]
    [Display(Name = "")]
    StateCompanyAnnualBudgetForecast = 209,

    [Description("شرکت هاي دولتي - تصميمات مجمع عمومي فوق العاده نوبت دوم")]
    [Display(Name = "")]
    StateCompanySecondEGMDecisions = 210,

    [Description("شرکت هاي دولتي - اطلاعات صورت مالي تلفيقي پايان دوره به همراه گزارش حسابرسي و بازرس قانوني")]
    [Display(Name = "")]
    StateCompanyConsolidatedFinancialStatements = 211,

    [Description("شرکت هاي دولتي - دعوت به مجمع عمومي فوق العاده نوبت دوم")]
    [Display(Name = "")]
    StateCompanySecondEGMInvitation = 212,

    [Description("شرکت هاي دولتي - اطلاعات صورت مالي ميان دوره اي")]
    [Display(Name = "")]
    StateCompanyInterimFinancialInformation = 213,

    [Description("شرکت هاي دولتي - درخواست ثبت صورتجلسه نزد مرجع ثبت شرکتها")]
    [Display(Name = "")]
    StateCompanyMinutesRegistrationRequest = 214,

    [Description("شرکت هاي دولتي - ساير موارد")]
    [Display(Name = "")]
    StateCompanyOther = 215,

    [Description("جزییات هزینه های انرژی")]
    [Display(Name = "N-74")]
    EnergyCostsDetails = 217,

    [Description("شرکتهاي بخش عمومی و سایر - اساسنامه شرکت")]
    [Display(Name = "")]
    PublicSectorCompanyCharter = 218,

    [Description("شرکتهاي بخش عمومی و سایر - تغييرات اساسنامه شرکت")]
    [Display(Name = "")]
    PublicSectorCompanyCharterChanges = 219,

    [Description("شرکتهاي بخش عمومی و سایر - گزارش توجيهي افزايش سرمايه به همراه گزارش حسابرس و بازرس قانوني")]
    [Display(Name = "")]
    PublicSectorCompanyCapitalIncreaseJustification = 220,

    [Description("شرکتهاي بخش عمومی و سایر - آگهي ثبت افزايش سرمايه")]
    [Display(Name = "")]
    PublicSectorCompanyCapitalIncreaseRegistrationNotice = 221,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عمومي عادي ساليانه")]
    [Display(Name = "")]
    PublicSectorCompanyAGMInvitation = 222,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عمومي عادي ساليانه نوبت دوم")]
    [Display(Name = "")]
    PublicSectorCompanySecondAGMInvitation = 223,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عادي به طور فوق العاده نوبت دوم")]
    [Display(Name = "")]
    PublicSectorCompanySecondOrdinaryExtraordinaryInvitation = 224,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عمومي عادي به طور فوق العاده")]
    [Display(Name = "")]
    PublicSectorCompanyOrdinaryExtraordinaryInvitation = 225,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عمومي فوق العاده")]
    [Display(Name = "")]
    PublicSectorCompanyEGMInvitation = 226,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عادي به طور فوق العاده")]
    [Display(Name = "")]
    PublicSectorCompanyOrdinaryExtraordinaryDecisions = 227,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عمومي عادي ساليانه")]
    [Display(Name = "")]
    PublicSectorCompanyAGMDecisions = 228,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عمومي عادي ساليانه نوبت دوم ")]
    [Display(Name = "")]
    PublicSectorCompanySecondAGMDecisions = 229,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عادي به طور فوق العاده نوبت دوم")]
    [Display(Name = "")]
    PublicSectorCompanySecondOrdinaryExtraordinaryDecisions = 230,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عمومي فوق العاده")]
    [Display(Name = "")]
    PublicSectorCompanyEGMDecisions = 231,

    [Description("شرکتهاي بخش عمومی و سایر - گزارش فعاليت هيات مديره به مجمع")]
    [Display(Name = "")]
    PublicSectorCompanyBoardReport = 232,

    [Description("شرکتهاي بخش عمومی و سایر - معرفي با تغيير ترکيب هيات مديره")]
    [Display(Name = "")]
    PublicSectorCompanyBoardChanges = 233,

    [Description("شرکتهاي بخش عمومی و سایر - اطلاعات و صورتهاي مالي پايان دوره به همراه گزارش حسابرس و بازرس قانوني")]
    [Display(Name = "")]
    PublicSectorCompanyYearEndFinancialStatements = 234,

    [Description("شرکتهاي بخش عمومی و سایر - صورت وضعيت پورتفو سرمايه گذاري ها در سهام و سهم الشرکه")]
    [Display(Name = "")]
    PublicSectorCompanyPortfolioStatement = 235,

    [Description("شرکتهاي بخش عمومی و سایر - پيش بيني بودجه سالانه")]
    [Display(Name = "")]
    PublicSectorCompanyAnnualBudgetForecast = 236,

    [Description("شرکتهاي بخش عمومی و سایر - تصميمات مجمع عمومي فوق العاده نوبت دوم")]
    [Display(Name = "")]
    PublicSectorCompanySecondEGMDecisions = 237,

    [Description("شرکتهاي بخش عمومی و سایر - اطلاعات صورت مالي تلفيقي پايان دوره به همراه گزارش حسابرسي و بازرس قانوني")]
    [Display(Name = "")]
    PublicSectorCompanyConsolidatedFinancialStatements = 238,

    [Description("شرکتهاي بخش عمومی و سایر - دعوت به مجمع عمومي فوق العاده نوبت دوم")]
    [Display(Name = "")]
    PublicSectorCompanySecondEGMInvitation = 239,

    [Description("شرکتهاي بخش عمومی و سایر - اطلاعات صورت مالي ميان دوره اي")]
    [Display(Name = "")]
    PublicSectorCompanyInterimFinancialInformation = 240,

    [Description("شرکتهاي بخش عمومی و سایر - درخواست ثبت صورتجلسه نزد مرجع ثبت شرکتها")]
    [Display(Name = "")]
    PublicSectorCompanyMinutesRegistrationRequest = 241,

    [Description("شرکتهاي بخش عمومی و سایر - ساير موارد")]
    [Display(Name = "")]
    PublicSectorCompanyOther = 242,

    [Description("اطلاع رسانی در خصوص علت تاخیر در ثبت افزایش سرمایه")]
    [Display(Name = "")]
    CapitalIncreaseRegistrationDelayDisclosure = 243,

    [Description("اطلاع رسانی در خصوص علت تاخیر در انتشار اعلامیه پذیره نویسی عمومی")]
    [Display(Name = "")]
    ProspectusPublicationDelayDisclosure = 244,

    [Description("اطلاع رسانی در خصوص تغییرات در برنامه افزایش سرمایه شرکت")]
    [Display(Name = "")]
    CapitalIncreaseProgramChangeDisclosure = 245,

    [Description("مشخصات کمیته انتصابات")]
    [Display(Name = "ن-85")]
    NominationCommitteeDetails = 248,

    [Description("شرکت هاي دولتي - اطلاعات صورت مالي ميان دوره اي حسابرسي شده")]
    [Display(Name = " ")]
    StateCompanyAuditedInterimFinancialInformation = 257,

    [Description("معافيت مالياتي بورسهاي کالايي")]
    [Display(Name = "")]
    CommodityExchangeTaxExemption = 258,

    [Description("خلاصه تصميمات مجمع عمومي عادي ساليانه")]
    [Display(Name = "ن-51")]
    AGMDecisionsSummary = 2020,

    [Description("خلاصه تصميمات مجمع عمومي عادي به‌طور فوق‌العاده")]
    [Display(Name = "ن-51")]
    OrdinaryExtraordinaryDecisionsSummary = 2121,

    [Description("خلاصه تصميمات مجمع عمومي فوق‌العاده")]
    [Display(Name = "")]
    EGMDecisionsSummary = 2222,

    [Description("مجوز بیمه مرکزی جهت برگزاری مجمع عمومی عادی سالیانه")]
    [Display(Name = "")]
    InsuranceOrganizationAGMPermission = 2223,

    [Description("نامشخص")]
    UnKnown = -1
}