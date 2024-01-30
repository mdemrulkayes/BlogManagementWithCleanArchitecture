using CleanArchitecture.BlogManagement.Core.Category;
using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Category.Create;
internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    public CreateCategoryCommandValidator(ICategoryRepository repository)
    {
        _repository = repository;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category Name can not be empty")
            .Length(5,50)
            .WithMessage("Category name length must be in 5 to 50 characters")
            .MustAsync(async (categoryName,ct) => await IsUniqueCategoryName(categoryName, ct))
            .WithMessage("Category name already exists");
    }

    private async Task<bool> IsUniqueCategoryName(string categoryName, CancellationToken cancellationToken = default)
    {
        return !await _repository.AnyAsync(x => x.Name.ToLower() == categoryName.ToLower());
    }
}
