using CleanArchitecture.BlogManagement.Application.Category.Create;
using CleanArchitecture.BlogManagement.Application.Category.Delete;
using CleanArchitecture.BlogManagement.Application.Category.Query;
using CleanArchitecture.BlogManagement.Application.Category.Update;
using CleanArchitecture.BlogManagement.WebApi.Extensions;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Category : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetCategories)
            .MapPost(CreateCategory)
            .MapPut(UpdateCategory, "{categoryId}")
            .MapDelete(DeleteCategory, "{categoryId}");
    }

    private static async Task<IResult> GetCategories(ISender sender)
    {
        var categories = await sender.Send(new GetAllCategoriesQuery());
        return Results.Ok(categories.Value);
    }

    private static async Task<IResult> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        var createdCategory = await sender.Send(command);
        return createdCategory.IsSuccess
            ? Results.Ok(createdCategory.Value)
            : createdCategory.ConvertToProblemDetails();
    }

    private static async Task<IResult> UpdateCategory(ISender sender, long categoryId, UpdateCategoryCommand command)
    {
        if (categoryId != command.CategoryId)
        {
            return Results.BadRequest("Invalid request");
        }

        var category = await sender.Send(command);
        return category.IsSuccess ? Results.Ok(category.Value) : category.ConvertToProblemDetails();
    }

    private static async Task<IResult> DeleteCategory(ISender sender, long categoryId)
    {
        var deleteCategory = await sender.Send(new DeleteCategoryCommand(categoryId));
        return deleteCategory.IsSuccess ? Results.Ok(deleteCategory.Value) : deleteCategory.ConvertToProblemDetails();
    }
}
