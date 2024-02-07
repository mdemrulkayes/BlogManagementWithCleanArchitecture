using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetPostDetailsById(long postId, CancellationToken cancellationToken = default);
    Task<Post?> GetPostDetailsWithoutComments(long postId, CancellationToken cancellationToken = default);
    Task<Comment?> GetCommentDetailsById(long  commentId, CancellationToken cancellationToken = default);
    Task<PaginatedList<Post>> GetAllPosts(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Post?> GetPostDetailsWithCategories(long postId, CancellationToken cancellationToken = default);
    Task<Post?> GetPostDetailsWithTags(long postId, CancellationToken cancellationToken = default);
}
