using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public struct PostErrors
{
    public static Error PostCanNotCreated = Error.Failure("Post.Create", "Post can not created");
    public static Error NotFound = Error.NotFound("Post.GetById", "Post details not found");

    public struct CommentErrors
    {
        public static Error CommentCanNotCreated = Error.Failure("Comment.Create", "Comment can not created");
        public static Error CommentNotFound = Error.NotFound("Comment.Update", "Comment details not found");
    }
}
