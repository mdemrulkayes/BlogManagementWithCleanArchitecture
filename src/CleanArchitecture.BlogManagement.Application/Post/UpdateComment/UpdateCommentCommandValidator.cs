using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
internal sealed class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CommentText)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CommentId)
            .NotEmpty()
            .NotNull();
    }
}
