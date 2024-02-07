using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Extensions;
using PostCore = CleanArchitecture.BlogManagement.Core.PostAggregate.Post;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence.Post;
internal sealed class PostRepository(BlogDbContext dbContext) : Repository<PostCore>(dbContext), IPostRepository
{
    private readonly BlogDbContext _dbContext = dbContext;

    public async Task<PostCore?> GetPostDetailsById(long postId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .Include(x => x.Comments
                .Take(5)
                .OrderByDescending(y => y.CreatedDate)
            )
            .Include(x => x.PostCategories)
            .ThenInclude(c => c.Category)
            .Include(x => x.PostTags)
            .ThenInclude(t => t.Tag)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PostId == postId, cancellationToken);
    }

    public async Task<PostCore?> GetPostDetailsWithoutComments(long postId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PostId == postId, cancellationToken);
    }

    public async Task<Comment?> GetCommentDetailsById(long commentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CommentId == commentId, cancellationToken);
    }

    public async Task<PaginatedList<PostCore>> GetAllPosts(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .Include(x => x.Comments
                .Take(5)
                .OrderByDescending(y => y.CreatedDate)
            )
            .Include(x => x.PostCategories)
            .ThenInclude(c => c.Category)
            .Include(x => x.PostTags)
            .ThenInclude(t => t.Tag)
            .OrderByDescending(x => x.CreatedDate)
            .ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<PostCore?> GetPostDetailsWithCategories(long postId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .Include(x => x.PostCategories)
            .FirstOrDefaultAsync(x => x.PostId == postId, cancellationToken);
    }

    public async Task<PostCore?> GetPostDetailsWithTags(long postId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .Include(x => x.PostTags)
            .FirstOrDefaultAsync(x => x.PostId == postId, cancellationToken);
    }
}
