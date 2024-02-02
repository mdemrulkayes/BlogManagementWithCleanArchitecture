using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostTag;
internal sealed class DeletePostTagCommandHandler(IPostRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePostTagCommand, Result<long>>
{
    public async Task<Result<long>> Handle(DeletePostTagCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsWithTags(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var removePostTag = postDetails.RemovePostTag(request.TagId);
        if (!removePostTag.IsSuccess || removePostTag.Value is null)
        {
            return removePostTag.Error;
        }

        await repository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
