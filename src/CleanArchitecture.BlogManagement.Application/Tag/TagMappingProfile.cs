using AutoMapper;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag;
internal class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<TagResponse, TagCore>();
    }
}
