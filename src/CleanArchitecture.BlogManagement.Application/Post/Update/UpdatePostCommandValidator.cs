using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.Update;
internal sealed class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Text)
            .NotEmpty();
    }
}
