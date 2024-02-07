using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.UnitTest;
public class PostUnitTest
{
    [Theory]
    [InlineData("Post 1", "Post description added here", "post-1-slug")]
    public void Create_Post_Should_ReturnNewPostResultObject(string title, string description, string slug)
    {
        var post = Post.CreatePost(title, slug, description);

        Assert.True(post.IsSuccess);

        Assert.NotNull(post.Value);

        Assert.IsType<Result<Post>>(post);
    }

    [Theory]
    [InlineData("Post 2", "Post 2 description added here", "post-2-slug")]
    public void Create_Post_Add_Category_Should_Return_Success_With_CategoryList(string title, string description, string slug)
    {
        //Arrange
        var expectedNumberOfCategories = 1;

        //Act
        var post = Post.CreatePost(title, slug, description);

        Assert.True(post.IsSuccess);

        Assert.NotNull(post.Value);

        Assert.Empty(post.Value.PostCategories);

        var category1 = Category.Category.Create("category1", "this is category description");

        Assert.NotNull(category1.Value);

        post.Value.AddPostCategory(category1.Value);

        //Assert

        Assert.True(post.IsSuccess);
        Assert.NotNull(post.Value.PostCategories);
        Assert.Equal(expectedNumberOfCategories, post.Value.PostCategories.Count);
    }

    [Fact]
    public void Create_Post_AddCategory_Value_Zero_Should_Return_InvalidError()
    {
        //Arrange
        //Act
        var post = Post.CreatePost("Post 2", "Post 2 description added here", "post-2-slug");

        Assert.NotNull(post.Value);

        var result = post.Value.AddPostCategory(null);

        //Assert
        Assert.Equal(PostErrors.PostCategoryErrors.InvalidCategory, result.Error);
    }

    [Fact]
    public void Delete_Post_Category_Return_Empty_PostCategories_From_PostDetails()
    {
        //Arrange
        var post = Post.CreatePost("New Post", "this is new post description", "new-post").Value;

        var category = Category.Category.Create("Category", "Category details").Value;

        post.AddPostCategory(category);

        Assert.NotEmpty(post.PostCategories);

        var removedCategory = post.RemovePostCategory(category.CategoryId);
        Assert.True(removedCategory.IsSuccess);
        Assert.NotNull(removedCategory.Value);
        Assert.Empty(post.PostCategories);
    }

    [Theory]
    [InlineData("asp.net core","")]
    public void Add_Post_Add_Tag_WithoutDescription_Should_Return_PostResult_WithTags(string tagName, string tagDescription)
    {
        //Arrange
        var post = CreatePost();
        Assert.NotNull(post.Value);

        var tag = Tag.Tag.Create(tagName, tagDescription);
        Assert.NotNull(tag.Value);
        //Act

        var addedTag = post.Value.AddPostTag(tag.Value);
        Assert.NotNull(addedTag);
        Assert.True(addedTag.IsSuccess);
        Assert.Equal(tagName, post.Value.PostTags.First().Tag.Name);
        Assert.NotEmpty(post.Value.PostTags);
    }

    [Theory]
    [InlineData("", "")]
    public void AddTagInPost_WithoutTagName_ShouldReturnValidationError(string tagName,
        string description)
    {
        var post = CreatePost();
        Assert.NotNull(post.Value);

        var tag = Tag.Tag.Create("Hello", description);
        Assert.NotNull(tag.Value);
        tag.Value.Update(tagName, description);

        var addedTag = post.Value.AddPostTag(tag.Value);
        Assert.Null(addedTag.Value);
        Assert.False(addedTag.IsSuccess);

        Assert.Equal(PostErrors.PostTagErrors.TagNameCanNotBeEmpty, addedTag.Error);
    }

    [Fact]
    public void RemoveTagFromPost_WithInvalidTagId_ShouldReturnValidationError()
    {
        var post = CreatePost();

        Assert.NotNull(post.Value);

        var removeTag = post.Value.RemovePostTag(154);
        Assert.False(removeTag.IsSuccess);
        Assert.Null(removeTag.Value);
        Assert.Empty(post.Value.PostTags);
        Assert.Equal(PostErrors.PostTagErrors.InvalidTag, removeTag.Error);
    }

    private Result<Post> CreatePost()
    {
        return Post.CreatePost("there is a post", "slug-post", "this is description");
    }
}
