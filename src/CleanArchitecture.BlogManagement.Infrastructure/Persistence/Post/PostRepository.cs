using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
                .Where(y => !y.IsDeleted)
                .Take(5)
                .OrderByDescending(y => y.CreatedDate)
            )
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

    public async Task<IEnumerable<PostCore>> GetAllPosts(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Posts
            .Include(x => x.Comments
                .Where(y => !y.IsDeleted)
                .Take(5)
                .OrderByDescending(y => y.CreatedDate)
            )
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }
}
