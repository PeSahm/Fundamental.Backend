using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.IncomeStatements.V7;
#pragma warning disable SA1402

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CodalIncomeStatement
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("incomeStatement")]
    public IncomeStatementDto IncomeStatement { get; set; }

    [JsonProperty("incomeStatementdesc")]
    public IncomeStatementdesc IncomeStatementDesc { get; set; }
}

public class IncomeStatementDto
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; }

    public void AddCustomRowItems()
    {
        int row = 1;

        foreach (RowItem rowItem in RowItems)
        {
            rowItem.RowNumber = row++;
        }
    }
}

public class IncomeStatementdesc
{
    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; }
}

public class RootCodalIncomeStatement
{
    [JsonProperty("incomeStatement")]
    public CodalIncomeStatement CodalIncomeStatement { get; set; }

    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; }

    public bool IsValidReport()
    {
        if (CodalIncomeStatement is null)
        {
            return false;
        }

        YearDatum? yearDatum = CodalIncomeStatement.IncomeStatement.YearData
            .Find(x => x.ColumnId == ColumnId.ThisPeriodData);

        if (yearDatum is null)
        {
            return false;
        }

        if (yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
        {
            return false;
        }

        return true;
    }
}

public class RowItem
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("value_9041")]
    public string Value9041 { get; set; }

    [JsonProperty("value_9042")]
    public string Value9042 { get; set; }

    [JsonProperty("value_9043")]
    public string Value9043 { get; set; }

    [JsonProperty("value_9045")]
    public string Value9045 { get; set; }

    [JsonProperty("value_9044")]
    public string Value9044 { get; set; }

    [JsonProperty("value_9411")]
    public string Value9411 { get; set; }

    public int RowNumber { get; set; }

    public string? GetDescription()
    {
        if (string.IsNullOrWhiteSpace(Value9041))
        {
            return null;
        }

        return Value9041.NormalizePersianText(
            PersianNormalizers.ApplyPersianYeKe
        );
    }

    public decimal GetValue(ColumnId columnId)
    {
        string propertyName = $"Value{(int)columnId}";
        object? thisValue = Array.Find(GetType().GetProperties(), info => info.Name == propertyName)?.GetValue(this);

        if (thisValue is null)
        {
            return 0;
        }

        return decimal.TryParse(thisValue.ToString(), out decimal res) ? res : 0;
    }
}

public class YearDatum
{
    [JsonProperty("columnId")]
    public ColumnId ColumnId { get; set; }

    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("periodEndToDate")]
    public string PeriodEndToDate { get; set; }

    [JsonProperty("yearEndToDate")]
    public string YearEndToDate { get; set; }

    [JsonProperty("period")]
    public int Period { get; set; }

    [JsonProperty("isAudited")]
    public bool IsAudited { get; set; }

    public DateOnly? YearEndToDateGeorgian => YearEndToDate.ToGregorianDateOnly();
    public DateOnly? PeriodEndToDateGeorgian => PeriodEndToDate.ToGregorianDateOnly();

    public int? FiscalYear => YearEndToDateGeorgian.GetPersianYear(false);

    public int? FiscalMonth => YearEndToDateGeorgian.GetPersianMonth(false);

    public int? ReportYear => PeriodEndToDateGeorgian.GetPersianYear(false);

    public int? ReportMonth => PeriodEndToDateGeorgian.GetPersianMonth(false);

    public decimal GetValue(ColumnId columnId)
    {
        string propertyName = $"Value{(int)columnId}";
        object? thisValue = Array.Find(GetType().GetProperties(), info => info.Name == propertyName)?.GetValue(this);

        if (thisValue is null)
        {
            return 0;
        }

        return decimal.Parse(thisValue.ToString() ?? "0");
    }
}

#pragma warning restore SA1649