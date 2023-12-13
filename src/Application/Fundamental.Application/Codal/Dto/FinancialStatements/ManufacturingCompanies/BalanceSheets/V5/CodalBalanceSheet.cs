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
    public BalanceSheet BalanceSheet { get; set; }
}

public class BalanceSheet
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; }
}

public class Root
{
    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; }

    [JsonProperty("balanceSheet")]
    public CodalBalanceSheet BalanceSheet { get; set; }
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

    public CustomRowCode CustomRowCode { get; set; }
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
        object? thisValue = GetType().GetProperties().FirstOrDefault(x => x.Name == propertyName)?.GetValue(this);

        if (thisValue is null)
        {
            return 0;
        }

        return decimal.Parse(thisValue.ToString() ?? "0");
    }
}

#pragma warning restore SA1649