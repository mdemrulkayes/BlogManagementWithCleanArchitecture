using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
internal sealed class UpdateCommentCommandHandler(IPostRepository repository, ICommentRepository commentRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateCommentCommand, Result<long>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<long>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsWithoutComments(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var commentDetails = await repository.GetCommentDetailsById(request.CommentId, cancellationToken);
        if (commentDetails is null)
        {
            return PostErrors.CommentErrors.CommentNotFound;
        }

        commentDetails.Update(request.CommentText);

        await commentRepository.Update(commentDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
