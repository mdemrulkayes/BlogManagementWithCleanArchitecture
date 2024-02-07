using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.UnitTest;
public sealed class TagUnitTest
{
    [Theory]
    [InlineData("Cefalo", "")]
    [InlineData("Employee", "Employee tag")]
    [InlineData("CSharp", "")]
    public void Create_Tag_With_TagName_Description_ShouldReturnTagObject(string tagName, string description)
    {
        //Arrange

        //Act

        var tagResult = Tag.Tag.Create(tagName, description);

        //Assert
        Assert.True(tagResult.IsSuccess);
        Assert.NotNull(tagResult.Value);

        Assert.Equal(tagName, tagResult.Value.Name);
        Assert.Equal(description, tagResult.Value.Description);

        Assert.IsType<Result<Tag.Tag>>(tagResult);
    }

    [Theory]
    [InlineData("")]
    public void Create_Tag_Without_TagName_Should_Return_Validation_ErrorObject(string tagName)
    {
        //Arrange
        var validationError = Error.Validation("Tag.Name", "Tag Name can not be empty");

        //Act
        var tagResult = Tag.Tag.Create(tagName);

        //Assert

        Assert.False(tagResult.IsSuccess);
        Assert.Null(tagResult.Value);

        Assert.NotNull(tagResult.Error);

        Assert.IsType<Result<Tag.Tag>>(tagResult);

        Assert.Equal(validationError, tagResult.Error);
    }

    [Theory]
    [InlineData("tagName", "desc")]
    public void Create_Tag_WithNameAndDescription_Should_Return_Error_When_Description_Length_IsLower(string tagName,
        string description)
    {
        //Arrange
        var validationError = Error.Validation("Tag.Description", "Tag Description can not be more than 150 characters");

        //Act
        var tagResult = Tag.Tag.Create(tagName, description);

        //Assert

        Assert.False(tagResult.IsSuccess);
        Assert.Null(tagResult.Value);

        Assert.NotNull(tagResult.Error);
        Assert.Equal(validationError, tagResult.Error);
    }

    [Theory]
    [InlineData("tagName", "Tag description")]
    public void Update_Tag_Should_Return_TagResultObject(string tagName, string description)
    {
        //Arrange
        var tag = Tag.Tag.Create(tagName, description).Value;

        Assert.NotNull(tag);
        //Act

        var updatedTag = tag.Update("Updated Tag Name", "Updated Tag description");

        //Assert

        Assert.True(updatedTag.IsSuccess);
        Assert.NotNull(updatedTag.Value);
        Assert.IsType<Result<Tag.Tag>>(updatedTag);

        Assert.NotEqual(updatedTag.Value.Name, tagName);
        Assert.NotEqual(updatedTag.Value.Description, description);
    }

}
