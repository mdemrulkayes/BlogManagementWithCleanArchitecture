using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public interface IPostRepository : IRepository
{
    Task<Post?> GetPostDetailsById(long postId, CancellationToken cancellationToken = default);
    Task<Post?> GetPostDetailsWithoutComments(long postId, CancellationToken cancellationToken = default);
    Task<Comment?> GetCommentDetailsById(long  commentId, CancellationToken cancellationToken = default);
}
