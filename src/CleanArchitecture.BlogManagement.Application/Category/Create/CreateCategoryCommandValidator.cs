using CleanArchitecture.BlogManagement.Core.Base;
using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Category.Create;
internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IRepository _repository;
    public CreateCategoryCommandValidator(IRepository repository)
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
        return !await _repository.AnyAsync<Core.Category.Category>(x => x.Name.ToLower() == categoryName.ToLower());
    }
}
