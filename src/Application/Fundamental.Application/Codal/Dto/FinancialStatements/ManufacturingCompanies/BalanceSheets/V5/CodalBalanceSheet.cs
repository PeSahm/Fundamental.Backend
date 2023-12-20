using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;
#pragma warning disable SA1402

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CodalBalanceSheet
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("balanceSheet")]
    public BalanceSheetDto BalanceSheet { get; set; }
}

public class BalanceSheetDto
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; } = new List<RowItem>();

    public void AddCustomRowItems()
    {
        int row = 1;

        foreach (RowItem rowItem in RowItems)
        {
            rowItem.RowNumber = row++;
        }
    }
}

public class RootCodalBalanceSheet
{
    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; }

    [JsonProperty("balanceSheet")]
    public CodalBalanceSheet? BalanceSheetData { get; set; }

    public bool IsValidReport()
    {
        if (BalanceSheetData is null)
        {
            return false;
        }

        YearDatum? yearDatum = BalanceSheetData.BalanceSheet.YearData
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
    public RowCode RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public Category Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("value_8991")]
    public string Value8991 { get; set; }

    [JsonProperty("value_8992")]
    public string Value8992 { get; set; }

    [JsonProperty("value_8993")]
    public string Value8993 { get; set; }

    [JsonProperty("value_8994")]
    public string Value8994 { get; set; }

    [JsonProperty("value_8995")]
    public string Value8995 { get; set; }

    public int RowNumber { get; set; }

    public string? GetDescription()
    {
        if (string.IsNullOrWhiteSpace(Value8991))
        {
            return null;
        }

        return Value8991.NormalizePersianText(
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

        return decimal.Parse(thisValue.ToString() ?? "0");
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
}

#pragma warning restore SA1649