using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;

namespace CleanArchitecture.BlogManagement.Application.Category.Create;
internal class CreateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<CreateCategoryCommand, Result<CategoryResponse>>
{
    public async Task<Result<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var category = Core.Category.Category.Create(request.Name, request.Description);
        if (!category.IsSuccess || category.Value is null)
        {
            return category.Error;
        }

        await repository.Add(category.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return mapper.Map<CategoryResponse>(category.Value);
    }
}
