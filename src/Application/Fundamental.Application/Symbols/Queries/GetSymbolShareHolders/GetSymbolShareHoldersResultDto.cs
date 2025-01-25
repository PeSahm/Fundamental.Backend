using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;

namespace Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;

public sealed class GetSymbolShareHoldersResultDto
{
    public required Guid Id { get; init; }
    public required string SymbolIsin { get; init; } = null!;
    public required string SymbolName { get; init; } = null!;
    public required string ShareHolderName { get; init; } = null!;
    public required decimal SharePercentage { get; init; }
    public required ReviewStatus ReviewStatus { get; init; }
    public string? ReviewStatusName => ReviewStatus.GetDescription();
    public string? ShareHolderSymbolIsin { get; init; }
    public string? ShareHolderSymbolName { get; init; }
}