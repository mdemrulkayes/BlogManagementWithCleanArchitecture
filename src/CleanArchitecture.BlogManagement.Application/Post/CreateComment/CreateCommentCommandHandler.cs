using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
internal sealed class CreateCommentCommandHandler(IRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCommentCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.FirstOrDefaultAsync<Core.PostAggregate.Post>(x => x.PostId == request.PostId, $"{nameof(Comment)}s");
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        postDetails.AddComment(request.Text);

        await repository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
