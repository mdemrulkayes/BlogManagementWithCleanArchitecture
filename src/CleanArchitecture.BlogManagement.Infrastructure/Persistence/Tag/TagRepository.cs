﻿using CleanArchitecture.BlogManagement.Core.Tag;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Extensions;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence.Tag;
internal sealed class TagRepository(BlogDbContext dbContext) : Repository<TagCore>(dbContext), ITagRepository
{
    private readonly BlogDbContext _dbContext = dbContext;

    public async Task<PaginatedList<TagCore>> GetAllTags(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Tags
            .ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<TagCore?> GetTagDetailsByText(string tagText, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Tags
            .FirstOrDefaultAsync(x => x.Name == tagText, cancellationToken);
    }
}
