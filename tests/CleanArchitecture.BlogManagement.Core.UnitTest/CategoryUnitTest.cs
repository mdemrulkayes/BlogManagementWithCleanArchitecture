using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.UnitTest;
public sealed class CategoryUnitTest
{
    [Theory]
    [InlineData("Test", "Test description")]
    public void Create_Category_Should_Return_CategoryObjectWithResult(string categoryName, string description)
    {
        //Arrange

        //Act

        var category = Category.Category.Create(categoryName, description);

        //Assert

        Assert.True(category.IsSuccess);
        Assert.NotNull(category.Value);
        Assert.Equal(categoryName, category.Value.Name);
        Assert.Equal(description, category.Value.Description);
        Assert.IsType<Result<Category.Category>>(category);
    }

    [Fact]
    public void Update_Category_Name_Should_Return_CategoryUpdatedObjectWithResult()
    {
        //Arrange
        var category = Category.Category.Create("Test", "Test Description");
        //Assert
        Assert.True(category.IsSuccess);
        Assert.NotNull(category.Value);
        //Act

        var updatedCategory = category.Value.Update("Test Update", "Test Description");

        //Assert

        Assert.True(updatedCategory.IsSuccess);
        Assert.NotNull(updatedCategory.Value);
        Assert.IsType<Result<Category.Category>>(updatedCategory);
        Assert.Equal(updatedCategory.Value.CategoryId, category.Value.CategoryId);

    }
}
