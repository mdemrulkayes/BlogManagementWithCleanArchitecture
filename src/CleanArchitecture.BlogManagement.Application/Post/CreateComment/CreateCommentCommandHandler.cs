using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
internal sealed class CreateCommentCommandHandler(IRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCommentCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.FirstOrDefaultAsync<Core.PostAggregate.Post>(x => x.PostId == request.PostId);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var comment = Comment.CreateComment(request.Text, request.PostId);

        if (!comment.IsSuccess || comment.Value is null)
        {
            return PostErrors.CommentErrors.CommentNotFound;
        }

        await repository.Add(comment.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
