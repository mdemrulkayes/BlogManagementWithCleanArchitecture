using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
internal sealed class UpdateCommentCommandHandler(IRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateCommentCommand, Result<long>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<long>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails =
            await repository.FirstOrDefaultAsync<Core.PostAggregate.Post>(x => x.PostId == request.PostId, $"{nameof(Comment)}s");
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var commentDetails = postDetails.Comments.FirstOrDefault(x => x.CommentId == request.CommentId);
        if (commentDetails is null)
        {
            return PostErrors.CommentErrors.CommentNotFound;
        }

        commentDetails.Update(request.CommentText);

        await repository.Update(commentDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
