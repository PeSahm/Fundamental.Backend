using Fundamental.Domain.Common.Dto;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<T> GetFirstPage<T>(IQueryable<T> queryable, PagingRequest request)
    {
        return queryable.Take(request.PageSize);
    }

    public static IQueryable<T> AsPaging<T>(this IQueryable<T> queryable, PagingRequest request)
    {
        return queryable
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
    }

    public static async Task<Paginated<T>> ToPagingListAsync<T>(
        this IQueryable<T> queryable,
        int pageSize,
        int pageNumber,
        string defaultSort = "",
        CancellationToken cancellationToken = default
    )
    {
        return await queryable.ToPagingListAsync(new PagingRequest(pageSize, pageNumber), defaultSort, cancellationToken);
    }

    public static async Task<Paginated<T>> ToPagingListAsync<T>(
        this IQueryable<T> queryable,
        PagingRequest request,
        string defaultSort = "",
        CancellationToken cancellationToken = default
    )
    {
        List<T> res = await queryable
            .ApplyOrderingAndPaging(new GridifyQuery(request.PageNumber, request.PageSize, string.Empty, request.OrderBy ?? defaultSort))
            .ToListAsync(cancellationToken);

        return new Paginated<T>(
            res,
            await queryable.GetPagingMetaData(request, cancellationToken)
        );
    }

    private static async Task<PaginationMeta> GetPagingMetaData<T>(
        this IQueryable<T> queryable,
        PagingRequest request,
        CancellationToken cancellationToken
    )
    {
        int totalCount = await queryable.CountAsync(cancellationToken);
        return new PaginationMeta(totalCount, ((request.PageNumber - 1) * request.PageSize) + 1, request.PageNumber * request.PageSize);
    }
}