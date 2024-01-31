using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostTag;
internal sealed class AddPostTagCommandValidator : AbstractValidator<AddPostTagCommand>
{
    public AddPostTagCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Tag)
            .NotEmpty();
    }
}
