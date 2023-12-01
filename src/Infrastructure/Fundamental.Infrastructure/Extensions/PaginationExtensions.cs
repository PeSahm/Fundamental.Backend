using Fundamental.Domain.Common.Dto;
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
        CancellationToken cancellationToken = default
    )
    {
        return await queryable.ToPagingListAsync(new PagingRequest(pageSize, pageNumber), cancellationToken);
    }

    public static async Task<Paginated<T>> ToPagingListAsync<T>(
        this IQueryable<T> queryable,
        PagingRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return new Paginated<T>(
            await queryable.AsPaging(request).ToListAsync(cancellationToken),
            await queryable.GetPagingMetaData(request, cancellationToken)
        );
    }

    private static async Task<PaginationMeta> GetPagingMetaData<T>(
        this IQueryable<T> queryable,
        PagingRequest request,
        CancellationToken cancellationToken
    )
    {
        int totalCount = await queryable.CountAsync(cancellationToken: cancellationToken);
        return new PaginationMeta(totalCount, ((request.PageNumber - 1) * request.PageSize) + 1, request.PageNumber * request.PageSize);
    }
}