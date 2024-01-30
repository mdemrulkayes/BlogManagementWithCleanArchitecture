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
        var expectedNumberOfCategories = 3;

        //Act
        var post = Post.CreatePost(title, slug, description);

        Assert.True(post.IsSuccess);

        Assert.NotNull(post.Value);

        Assert.Empty(post.Value.PostCategories);

        post.Value.AddPostCategory([1, 2, 3]);

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

        var result = post.Value.AddPostCategory([0, 2, 4]);

        //Assert
        Assert.Equal(PostErrors.PostCategoryErrors.InvalidCategoryId, result.Error);
    }
}
