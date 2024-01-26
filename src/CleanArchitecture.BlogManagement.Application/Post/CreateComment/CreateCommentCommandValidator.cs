using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
internal sealed class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();
    }
}
