using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
internal sealed class CreateCommentCommandHandler(IPostRepository repository, ICommentRepository commentRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCommentCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsWithoutComments(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var comment = Comment.CreateComment(request.Text, request.PostId);

        if (!comment.IsSuccess || comment.Value is null)
        {
            return PostErrors.CommentErrors.CommentNotFound;
        }

        await commentRepository.Add(comment.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
