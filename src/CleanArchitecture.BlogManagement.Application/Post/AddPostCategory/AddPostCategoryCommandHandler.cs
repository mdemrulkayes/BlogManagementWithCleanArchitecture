using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
internal sealed class AddPostCategoryCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddPostCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(AddPostCategoryCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await postRepository.GetPostDetailsWithoutComments(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var postCategoryAddedResult = postDetails.AddPostCategory(request.CategoryIds.ToList());

        if (!postCategoryAddedResult.IsSuccess || postCategoryAddedResult.Value is null)
        {
            return postCategoryAddedResult.Error;
        }

        await postRepository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
