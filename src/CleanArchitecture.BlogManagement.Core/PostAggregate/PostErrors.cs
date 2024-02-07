using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public struct PostErrors
{
    public static Error PostCanNotCreated = Error.Failure("Post.Create", "Post can not created");
    public static Error NotFound = Error.NotFound("Post.GetById", "Post details not found");

    public static Error PostCategoriesAreRequired =
        Error.Failure("Post.Category", "Post category can not empty while adding new category");

    public struct CommentErrors
    {
        public static Error CommentCanNotCreated = Error.Failure("Comment.Create", "Comment can not created");
        public static Error CommentNotFound = Error.NotFound("Comment.Update", "Comment details not found");
    }

    public struct PostCategoryErrors
    {
        public static Error InvalidCategory = Error.Validation("Post.PostCategory", "Invalid category");
    }

    public struct PostTagErrors
    {
        public static Error InvalidTag = Error.Validation("Post.PostTag", "Invalid tag");
        public static Error TagAlreadyAdded = Error.Validation("Post.PostTag", "Tag already added to the post");
        public static Error TagNameCanNotBeEmpty = Error.Validation("Post.PostTag", "Tag name is required");
        public static Error TagDescriptionLengthShouldBeMoreThan10 = Error.Validation("Post.PostTag", "Tag description length should be more than 10 characters");

    }
}
