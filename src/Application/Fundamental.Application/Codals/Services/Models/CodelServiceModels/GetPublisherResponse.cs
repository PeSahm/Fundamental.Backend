using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Services.Models.CodelServiceModels;

public class GetPublisherResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("displayedSymbol")]
    public string DisplayedSymbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("parent")]
    public string Parent { get; set; }

    [JsonProperty("isic")]
    public string Isic { get; set; }

    [JsonProperty("reportingType")]
    public int ReportingType { get; set; }

    [JsonProperty("companyType")]
    public int CompanyType { get; set; }

    [JsonProperty("executiveManager")]
    public string ExecutiveManager { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("telNo")]
    public string TelNo { get; set; }

    [JsonProperty("faxNo")]
    public string FaxNo { get; set; }

    [JsonProperty("activitySubject")]
    public string ActivitySubject { get; set; }

    [JsonProperty("officeAddress")]
    public string OfficeAddress { get; set; }

    [JsonProperty("shareOfficeAddress")]
    public string ShareOfficeAddress { get; set; }

    [JsonProperty("website")]
    public string Website { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("state")]
    public int State { get; set; }

    [JsonProperty("stateName")]
    public string StateName { get; set; }

    [JsonProperty("inspector")]
    public string Inspector { get; set; }

    [JsonProperty("financialManager")]
    public string FinancialManager { get; set; }

    [JsonProperty("factoryTel")]
    public string FactoryTel { get; set; }

    [JsonProperty("factoryFax")]
    public string FactoryFax { get; set; }

    [JsonProperty("officeTel")]
    public string OfficeTel { get; set; }

    [JsonProperty("officeFax")]
    public string OfficeFax { get; set; }

    [JsonProperty("shareOfficeTel")]
    public string ShareOfficeTel { get; set; }

    [JsonProperty("shareOfficeFax")]
    public string ShareOfficeFax { get; set; }

    [JsonProperty("nationalCode")]
    public string NationalCode { get; set; }

    [JsonProperty("isinCode")]
    public string IsinCode { get; set; }

    [JsonProperty("financialYear")]
    public string FinancialYear { get; set; }

    [JsonProperty("listedCapital")]
    public decimal ListedCapital { get; set; }

    [JsonProperty("auditorName")]
    public string AuditorName { get; set; }

    [JsonProperty("isEnableSubCompany")]
    public int IsEnableSubCompany { get; set; }

    [JsonProperty("isEnabled")]
    public bool IsEnabled { get; set; }

    [JsonProperty("fundType")]
    public int FundType { get; set; }

    [JsonProperty("fundTypeTitle")]
    public object FundTypeTitle { get; set; }

    [JsonProperty("subcompanyType")]
    public int SubCompanyType { get; set; }

    [JsonProperty("isSupplied")]
    public bool IsSupplied { get; set; }

    [JsonProperty("marketType")]
    public int MarketType { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public decimal UnauthorizedCapital { get; set; }
}