using AutoMapper;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Common.Mapping;
internal sealed class PaginatedListTypeConverter<T> 
    : ITypeConverter<PaginatedList<T>, PagedListDto<T>> 
    where T : class
{
    public PagedListDto<T> Convert(PaginatedList<T> source, PagedListDto<T> destination, ResolutionContext context)
    {
        return new PagedListDto<T>
        {
            Items = source.Items,
            TotalCount = source.TotalCount
        };
    }
}
