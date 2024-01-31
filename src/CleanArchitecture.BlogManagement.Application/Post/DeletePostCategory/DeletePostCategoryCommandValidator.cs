using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostCategory;
internal sealed class DeletePostCategoryCommandValidator : AbstractValidator<DeletePostCategoryCommand>
{
    public DeletePostCategoryCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
