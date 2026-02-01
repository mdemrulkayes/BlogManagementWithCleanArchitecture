using Asp.Versioning;
using CleanArchitecture.BlogManagement.Application.Category;
using CleanArchitecture.BlogManagement.Application.Category.Create;
using CleanArchitecture.BlogManagement.Application.Category.Delete;
using CleanArchitecture.BlogManagement.Application.Category.Query;
using CleanArchitecture.BlogManagement.Application.Category.Update;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Category : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(ApiVersion.Default)
            .Build();

        builder.MapGroup(this)
            .WithApiVersionSet(apiVersionSet)
            //.RequireAuthorization()
            .MapGet(GetCategories, responseType: typeof(PagedListDto<CategoryResponse>))
            .MapPost(CreateCategory, responseType: typeof(CategoryResponse))
            .MapPut(UpdateCategory, "{categoryId}", responseType: typeof(CategoryResponse))
            .MapDelete(DeleteCategory, "{categoryId}", responseType: typeof(bool));
    }

    private static async Task<IResult> GetCategories(ISender sender, GetAllCategoriesQuery query)
    {
        var categories = await sender.Send(query);
        return ReturnResultValue(categories);
    }

    private static async Task<IResult> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        var createdCategory = await sender.Send(command);
        return ReturnResultValue(createdCategory);
    }

    private static async Task<IResult> UpdateCategory(ISender sender, long categoryId, UpdateCategoryCommand command)
    {
        if (categoryId != command.CategoryId)
        {
            return Results.BadRequest("Invalid request");
        }

        var category = await sender.Send(command);
        return ReturnResultValue(category);
    }

    private static async Task<IResult> DeleteCategory(ISender sender, long categoryId)
    {
        var deleteCategory = await sender.Send(new DeleteCategoryCommand(categoryId));
        return ReturnResultValue(deleteCategory);
    }
}
