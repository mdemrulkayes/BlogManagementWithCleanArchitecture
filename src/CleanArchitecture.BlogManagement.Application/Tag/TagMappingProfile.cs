using AutoMapper;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag;
internal class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<TagCore, TagResponse>()
            .ConstructUsing(tag => new TagResponse(
                tag.TagId,
                tag.Name,
                tag.Description
            ))
            .ReverseMap();
    }
}
