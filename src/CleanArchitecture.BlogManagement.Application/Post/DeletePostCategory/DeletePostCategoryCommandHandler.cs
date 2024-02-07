using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostCategory;
internal sealed class DeletePostCategoryCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePostCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(DeletePostCategoryCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await postRepository.GetPostDetailsWithCategories(request.PostId, cancellationToken);
        if (postDetails == null)
        {
            return PostErrors.NotFound;
        }

        var removePostCategory = postDetails.RemovePostCategory(request.CategoryId);
        if (!removePostCategory.IsSuccess || removePostCategory.Value is null)
        {
            return removePostCategory.Error;
        }

        await postRepository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
