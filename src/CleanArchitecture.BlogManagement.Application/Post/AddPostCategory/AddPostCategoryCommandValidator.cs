using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
internal sealed class AddPostCategoryCommandValidator : AbstractValidator<AddPostCategoryCommand>
{
    public AddPostCategoryCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
