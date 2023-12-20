namespace Fundamental.Domain.Common.Dto;

public record PagingRequest(int PageSize = 100, int PageNumber = 1, string? OrderBy = null);