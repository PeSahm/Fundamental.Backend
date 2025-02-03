using System.Text.Json.Serialization;
using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;

public sealed class GetStatusOfViableCompaniesResultDto
{
    public required Guid Id { get; init; }
    public required string ParentSymbolIsin { get; init; }

    public required string PatentSymbol { get; init; }

    public required string ParentSymbolName { get; init; }

    public required string SubsidiarySymbolName { get; init; }

    public required decimal OwnershipPercentage { get; init; }

    public required decimal? OwnershipPercentageProvidedByAdmin { get; init; }

    public required decimal CostPrice { get; set; }

    public string? SubsidiarySymbolIsin { get; init; }

    public required ReviewStatus ReviewStatus { get; init; }

    public string? ReviewStatusName => ReviewStatus.GetDescription();

    public required string? Url { get; init; }

    [JsonIgnore]
    public DateTime? CreatedAt { get; init; }
}