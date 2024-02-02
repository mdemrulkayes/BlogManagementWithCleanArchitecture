using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Common.Mapping;
internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PaginatedList<>), typeof(PagedListDto<>));
    }
}
