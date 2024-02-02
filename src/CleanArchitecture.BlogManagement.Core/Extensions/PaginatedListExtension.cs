﻿using CleanArchitecture.BlogManagement.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.BlogManagement.Core.Extensions;
public static class PaginatedListExtension
{
    public static Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber,
        int pageSize, CancellationToken cancellationToken = default) where T : class
        => PaginatedList<T>.CreatePaginatedListAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);
}
