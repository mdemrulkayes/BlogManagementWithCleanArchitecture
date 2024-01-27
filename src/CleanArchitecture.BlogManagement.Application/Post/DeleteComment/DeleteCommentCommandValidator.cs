using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Post.DeleteComment;
internal sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CommentId)
            .NotEmpty()
            .NotNull();
    }
}
