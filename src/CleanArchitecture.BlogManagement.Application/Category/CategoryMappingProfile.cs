using AutoMapper;

namespace CleanArchitecture.BlogManagement.Application.Category;
internal sealed class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Core.Category.Category, CategoryResponse>()
            .ConstructUsing(category => new CategoryResponse(category.CategoryId, category.Name, category.Description))
            .ReverseMap();
    }
}
