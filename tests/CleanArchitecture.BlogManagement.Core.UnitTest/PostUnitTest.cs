using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

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
}
