namespace Fundamental.Domain.Common.Dto;

public record Paginated<T>(List<T> Items, PaginationMeta Meta);