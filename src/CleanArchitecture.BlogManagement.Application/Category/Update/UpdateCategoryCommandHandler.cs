using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category.Update;
internal sealed class UpdateCategoryCommandHandler(IRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<UpdateCategoryCommand, Result<CategoryResponse>>
{
    public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =
            await repository.FirstOrDefaultAsync<Core.Category.Category>(x => x.CategoryId == request.CategoryId);
        if (category == null)
        {
            return CategoryErrors.NotFound;
        }

        var updatedCategory = category.Update(request.Name, request.Description);
        if (!updatedCategory.IsSuccess || updatedCategory.Value is null)
        {
            return updatedCategory.Error;
        }
        await repository.Update(updatedCategory.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return mapper.Map<CategoryResponse>(updatedCategory.Value);
    }
}
