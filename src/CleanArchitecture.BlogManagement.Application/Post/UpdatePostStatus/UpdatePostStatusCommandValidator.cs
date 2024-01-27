using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdatePostStatus;
internal sealed class UpdatePostStatusCommandValidator : AbstractValidator<UpdatePostStatusCommand>
{
    public UpdatePostStatusCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Status)
            .NotNull()
            .IsInEnum();
    }
}
