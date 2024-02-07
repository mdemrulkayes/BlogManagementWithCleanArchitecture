using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Core.PostAggregate.Events;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
internal sealed class CreateCommentCommandHandler(
    IPostRepository repository,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateCommentCommandHandler> logger) : ICommandHandler<CreateCommentCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsWithoutComments(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            logger.LogWarning("@{ErrorMessage} with @{PostId}", PostErrors.NotFound, request.PostId);
            return PostErrors.NotFound;
        }

        var comment = Comment.CreateComment(request.Text, request.PostId);

        if (!comment.IsSuccess || comment.Value is null)
        {
            return PostErrors.CommentErrors.CommentNotFound;
        }

        comment.Value.RaiseEvents(new CommentAdded(Guid.NewGuid(), comment.Value.Text));

        await commentRepository.Add(comment.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
