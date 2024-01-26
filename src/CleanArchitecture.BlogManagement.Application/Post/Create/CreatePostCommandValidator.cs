using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.Create;
internal sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
        RuleFor(x => x.Slug)
            .NotEmpty();
        RuleFor(x => x.Text)
            .NotEmpty();
        RuleFor(x => x.Status)
            .NotEmpty()
            .IsInEnum();
    }
}
