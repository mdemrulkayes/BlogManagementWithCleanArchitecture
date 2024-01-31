using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
internal sealed class AddPostCategoryCommandHandler(IPostRepository postRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddPostCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(AddPostCategoryCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await postRepository.GetPostDetailsWithCategories(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        if (postDetails.PostCategories.Any(x => x.CategoryId == request.CategoryId))
        {
            return postDetails.PostId;
        }

        var selectedCategory = await categoryRepository.GetCategoriesByIds(request.CategoryId, cancellationToken);

        if (selectedCategory is null)
        {
            return PostErrors.PostCategoryErrors.InvalidCategory;
        }

        var postCategoryAddedResult = postDetails.AddPostCategory(selectedCategory);

        if (!postCategoryAddedResult.IsSuccess || postCategoryAddedResult.Value is null)
        {
            return postCategoryAddedResult.Error;
        }

        await postRepository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
