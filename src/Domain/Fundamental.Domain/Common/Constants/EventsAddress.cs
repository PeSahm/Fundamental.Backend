namespace Fundamental.Domain.Common.Constants;

public static class EventsAddress
{
    public static class ClosePrice
    {
        public const string PRICE_UPDATE = "ClosePrice.Update";
        public const string ADJUSTED_PRICE_UPDATE = "Adjusted.ClosePrice.Update";
    }

    public static class FinancialStatement
    {
        public const string FINANCIAL_STATEMENT_EVENT_GROUP = "FinancialStatement";
    }

    public static class MonthlyActivity
    {
        public const string MONTHLY_ACTIVITY_UPDATE = "monthly-activity.monthly-activity-update";
    }

    public static class CapitalIncrease
    {
        public const string CAPITAL_INCREASE_NOTICE_UPDATE = "capital.increase.notice.update";
    }
}