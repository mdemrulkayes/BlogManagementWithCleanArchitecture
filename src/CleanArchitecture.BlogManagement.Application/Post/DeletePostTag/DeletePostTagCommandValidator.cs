using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostTag;
internal sealed class DeletePostTagCommandValidator : AbstractValidator<DeletePostTagCommand>
{
    public DeletePostTagCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.TagId)
            .NotNull()
            .GreaterThan(0);
    }
}
